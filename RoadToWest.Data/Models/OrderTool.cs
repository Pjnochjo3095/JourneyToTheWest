using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class OrderTool
    {
        public string SceneId { get; set; }
        public string ToolId { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int? Amount { get; set; }
        public string Status { get; set; }
        public string Author { get; set; }
        public DateTime? LastModified { get; set; }
        public string Id { get; set; }

        public virtual Scene Scene { get; set; }
        public virtual Tool Tool { get; set; }
    }
}
