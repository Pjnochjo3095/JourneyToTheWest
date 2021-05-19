using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.ViewModels
{
    public class ActorCreateModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }

    }
    public class ActorViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("status")]
        public bool? Status { get; set; }
       
    }
    public class ActorUpdateModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }
    public class ActorFilter
    {
        [JsonProperty("name_contains")]
        public string Name_Contains { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
    public class ActorSort
    {
        public const string Name = "name";
    }

    public class CharacterViewModel
    {
        [JsonProperty("sceneid")]
        public string SceneId { get; set; }
        [JsonProperty("scene_name")]
        public string SceneName { get; set; }
        [JsonProperty("character")]
        public string Character { get; set; }
        [JsonProperty("script_link")]
        public string script_link { get; set; }
    }




}
