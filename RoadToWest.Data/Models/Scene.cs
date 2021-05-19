using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class Scene
    {
        public Scene()
        {
            OrderTool = new HashSet<OrderTool>();
            SceneCharacter = new HashSet<SceneCharacter>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int? SnapShot { get; set; }
        public string Status { get; set; }

        public virtual ICollection<OrderTool> OrderTool { get; set; }
        public virtual ICollection<SceneCharacter> SceneCharacter { get; set; }
    }
}
