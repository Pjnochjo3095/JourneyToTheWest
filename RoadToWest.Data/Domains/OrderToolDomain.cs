using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using RoadToWest.Data.Repositories;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Domains
{
    public class OrderToolDomain
    {
        private IOrderToolRepository _orderRepo;
        private ToolDomain _toolDomain;
        private DbContext context;
        public OrderToolDomain(IOrderToolRepository orderToolRepository, ToolDomain toolDomain, DbContext context)
        {
            this._orderRepo = orderToolRepository;
            this._toolDomain = toolDomain;
            this.context = context;
        }
        public bool CreateToolScene(ToolSceneViewModel model)
        {
            var check = false;
           
            using (var trans = context.Database.BeginTransaction())
            {
                
                foreach (BorrowToolModel borrowTool in model.Tools)
                {
                    
                    var tool = _toolDomain.Get().FirstOrDefault(s => s.Id == borrowTool.ToolId);
                    if(tool.Amount < borrowTool.Amount)
                    {
                        return false;
                    }
                    else
                    {
                        _orderRepo.CreateOrder(borrowTool, model.SceneId, model.Author);
                        _toolDomain.BorrowTool(borrowTool.ToolId, borrowTool.Amount);
                        context.SaveChanges();
                    }
                    
                }
                trans.Commit();
                check = true;
            }
            return check;
        }
        public List<ToolViewContentModel> GetTools(string sceneId) 
        {
            List<ToolViewContentModel> result = new List<ToolViewContentModel>();

            var orderTools = _orderRepo.Get().Where(s => s.SceneId ==  sceneId && s.Status != OrderStatus.DELETED);

            foreach(var orderTool in orderTools)
            {
                ToolViewContentModel model = new ToolViewContentModel
                {
                    Id = orderTool.Id,
                    SceneId = sceneId,
                    Name = orderTool.Tool.Name,
                    SceneName = orderTool.Scene.Name,
                    ToolId = orderTool.ToolId,
                    BorrowFrom = orderTool.TimeStart,
                    BorrowTo = orderTool.TimeEnd,
                    Amount = orderTool.Amount,
                    Image = orderTool.Tool.Image,
                    Status = orderTool.Status
                };
                result.Add(model);
            }
            return result;
        }
        public bool DeleteOrder(String id)
        {
            var check = false;
            using(var trans = context.Database.BeginTransaction())
            {
                var order = _orderRepo.Get().FirstOrDefault(s => s.Id == id);
                order.Status = OrderStatus.DELETED;
                _orderRepo.Update(order);
                _toolDomain.LendTool(order.ToolId, order.Amount);
                context.SaveChanges();
                trans.Commit();
                check = true;
            }
            return check;
        }
        public List<ToolViewContentModel> GetToolsByDate(DateTime begin, DateTime end)
        {
            List<ToolViewContentModel> result = new List<ToolViewContentModel>();

            var orderTools = _orderRepo.Get().Where(s=> DateTime.Compare(begin, (DateTime)s.TimeStart ) <= 0 && DateTime.Compare(end, (DateTime)s.TimeStart) >= 0).ToList();

            foreach (var orderTool in orderTools)
            {
                ToolViewContentModel model = new ToolViewContentModel
                {
                    Id = orderTool.Id,
                    Name = orderTool.Tool.Name,
                    SceneName = orderTool.Scene.Name,
                    ToolId = orderTool.ToolId,
                    BorrowFrom = orderTool.TimeStart,
                    BorrowTo = orderTool.TimeEnd,
                    Amount = orderTool.Amount,
                    Image = orderTool.Tool.Image,
                    Status = orderTool.Status,
                    SceneId = orderTool.SceneId
                };
                result.Add(model);
            }
            return result;
        }
    }
}
