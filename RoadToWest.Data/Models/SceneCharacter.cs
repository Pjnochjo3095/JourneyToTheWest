using System;
using System.Collections.Generic;

namespace RoadToWest.Data.Models
{
    public partial class SceneCharacter
    {
        public string SceneId { get; set; }
        public string ActorId { get; set; }
        public string Id { get; set; }
        public string Character { get; set; }
        public string Description { get; set; }
        public string ScriptLink { get; set; }
        public string Status { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Scene Scene { get; set; }
    }
}
