using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class Actor
    {
        public Actor()
        {
            SceneCharacter = new HashSet<SceneCharacter>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<SceneCharacter> SceneCharacter { get; set; }
    }
}
