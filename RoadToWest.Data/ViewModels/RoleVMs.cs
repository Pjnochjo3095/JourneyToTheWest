using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.ViewModels
{
    public class RoleViewModel
    {
        [JsonProperty("role_id")]
        public string Id { get; set; }

        [JsonProperty("role_name")]
        public string Name { get; set; }
    }
}
