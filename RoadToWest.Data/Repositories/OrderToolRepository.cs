using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.Repositories
{
    public partial interface IOrderToolRepository : IBaseRepository<OrderTool, string>
    {
        OrderTool CreateOrder(BorrowToolModel model, string sceneId, string author);
        OrderTool PrepareOrder(BorrowToolModel model, string sceneId, string author);
    }
    public class OrderToolRepository : BaseRepository<OrderTool, string>, IOrderToolRepository
    {
        public OrderToolRepository(DbContext context) : base(context)
        {
        }

        public OrderTool CreateOrder(BorrowToolModel model, string sceneId, string author)
        {
            var newOrder = PrepareOrder(model, sceneId, author);
            return Create(newOrder).Entity;
        }

        public OrderTool PrepareOrder(BorrowToolModel model, string sceneId, string author)
        {
            var newOrder = new OrderTool
            {
                Id = Guid.NewGuid().ToString(),
                TimeStart = model.BorrowFrom,
                TimeEnd = model.BorrowTo,
                SceneId = sceneId,
                ToolId = model.ToolId,
                Amount = model.Amount,
                Author = author,
                Status = "New",
                LastModified = DateTime.Now
            };
            return newOrder;
        }
    }
}
