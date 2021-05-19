using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Extensions;
using RoadToWest.Data.Models;
using RoadToWest.Data.Repositories;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Domains
{
    public class ToolDomain
    {
        private IToolRepository _toolRepo;

        public ToolDomain(IToolRepository ToolRepository)
        {
            this._toolRepo = ToolRepository;
        }
        public Tool CreateTool(ToolCreateModel model, string image)
        {
            var newTool = _toolRepo.CreateTool(model, image);
            return newTool;
        }


        public DbSet<Tool> Get()
        {
            return _toolRepo.Get();
        }
        public object GetTool(ToolFilter filter, string sort)
        {
            var query = Get();
            return query.GetData(filter, sort);
        }
        public Tool Update(string toolId, ToolUpdateModel model, string image)
        {
            var tool = _toolRepo.Get().Where(s => s.Id == toolId).FirstOrDefault();
            if (tool != null)
            {
                var updateTool = _toolRepo.EditTool(tool, model, image);
                return _toolRepo.Update(updateTool).Entity;
            }
            else
            {
                return null;
            }
        }
        public Tool Delete(string toolId)
        {
            var tool = _toolRepo.Get().Where(s => s.Id == toolId).FirstOrDefault();
            if (tool != null)
            {
                var deleteTool = _toolRepo.DeleteTool(tool);
                return _toolRepo.Update(deleteTool).Entity;
            }
            return null;
        }
        public ToolViewModel GetToolById(string toolId)
        {
            var tool = _toolRepo.Get().Where(s => s.Id == toolId).FirstOrDefault();
            if (tool != null)
            {
                var result = new ToolViewModel
                {
                    Id = tool.Id,
                    Name = tool.Name,
                    Amount = tool.Amount,
                    Description = tool.Description,
                    Image = tool.Image,
                    Status = tool.Status
                };
                return result;
            }
            return null;
        }
        public bool BorrowTool(string toolId, int amount)
        {
            var tool = _toolRepo.Get().FirstOrDefault(s => s.Id == toolId);
            if(tool != null)
            {
                if(tool.Amount == 0 || tool.Amount < amount)
                {
                    return false;
                }
                else
                {
                    tool.Amount = tool.Amount - amount;
                    if(tool.Amount == 0)
                    {
                        tool.Status = ToolStatus.OUTOFSTOCK;
                    }
                    _toolRepo.Update(tool);
                    return true;
                }
            }
            return false;
        }
        public bool LendTool(string toolId, int? amount)
        {
            var tool = _toolRepo.Get().FirstOrDefault(s => s.Id == toolId);
            if (tool != null)
            {
                    tool.Amount = tool.Amount + amount;
                if (tool.Status == ToolStatus.OUTOFSTOCK)
                {
                    tool.Status = ToolStatus.AVAILABLE;
                }
                    _toolRepo.Update(tool);
                    return true;
                }
            return false;
        }

    }
}
