using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.Repositories
{
    public partial interface IToolRepository : IBaseRepository<Tool, String>
    {
        Tool PrepareCreate(ToolCreateModel model);
        Tool CreateTool(ToolCreateModel model, string image);

        Tool EditTool(Tool entity, ToolUpdateModel model, string image);
        Tool DeleteTool(Tool entity);

    }
    public partial class ToolRepository : BaseRepository<Tool, String>, IToolRepository
    {
        public ToolRepository(DbContext context) : base(context)
        {
        }

        public Tool CreateTool(ToolCreateModel model, string image)
        {
            Tool tool = PrepareCreate(model);
            tool.Image = image;
            return Create(tool).Entity;
        }

        public Tool DeleteTool(Tool entity)
        {
            entity.Status = ToolStatus.DELETED;
            return entity;
        }

        public Tool EditTool(Tool entity, ToolUpdateModel model, string image)
        {
            entity.Name = model.Name;
            entity.Image = image;
            entity.Amount = model.Amount;
            entity.Description = model.Description;
            return entity;
        }

        public Tool PrepareCreate(ToolCreateModel model)
        {
            Tool Tool = new Tool
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                Amount = model.Amount,
                Status = ToolStatus.AVAILABLE,
            };
            return Tool;
        }
    }
}
