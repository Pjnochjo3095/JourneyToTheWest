using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.ViewModels
{
    public class ToolCreateModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }
    public class ToolUpdateModel
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }
    public class ToolFilter
    {
        [JsonProperty("name_contains")]
        public string Name_Contains { get; set; }
        [JsonProperty("ids")]
        public string[] Ids { get; set; }
    }
    public class ToolFieldsSort
    {
        public const string Name = "name";
    }
    public class ToolViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public int? Amount { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

    }
}
