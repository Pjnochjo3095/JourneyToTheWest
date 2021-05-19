using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class Roles
    {
        public Roles()
        {
            UserRole = new HashSet<UserRole>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
