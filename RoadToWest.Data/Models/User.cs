using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class User
    {
        public User()
        {
            History = new HashSet<History>();
            UserRole = new HashSet<UserRole>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public bool? IsActor { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
