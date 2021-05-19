using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class Tool
    {
        public Tool()
        {
            OrderTool = new HashSet<OrderTool>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? Amount { get; set; }
        public string Status { get; set; }

        public virtual ICollection<OrderTool> OrderTool { get; set; }
    }
}
