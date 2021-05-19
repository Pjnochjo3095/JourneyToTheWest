using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class History
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime? Modified { get; set; }
        public string Author { get; set; }

        public virtual User User { get; set; }
    }
}
