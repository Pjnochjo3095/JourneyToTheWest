using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class UserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Id { get; set; }

        public virtual Roles Role { get; set; }
        public virtual User User { get; set; }
    }
}
