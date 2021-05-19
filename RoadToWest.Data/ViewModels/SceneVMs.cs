using Newtonsoft.Json;
using System;

namespace RoadToWest.Data.ViewModels
{
    public class SceneCreateModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("time_start")]
        public DateTime TimeStart { get; set; }

        [JsonProperty("time_end")]
        public DateTime TimeEnd { get; set; }
        [JsonProperty("snapshot")]
        public int Snapshot { get; set; }
    }
    public class SceneViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("time_start")]
        public DateTime? TimeStart { get; set; }

        [JsonProperty("time_end")]
        public DateTime? TimeEnd { get; set; }
        [JsonProperty("snapshot")]
        public int? Snapshot { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("character")]
        public string Character { get; set; }
    }
    public class SceneUpdateModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("time_start")]
        public DateTime TimeStart { get; set; }

        [JsonProperty("time_end")]
        public DateTime TimeEnd { get; set; }
        [JsonProperty("snapshot")]
        public int Snapshot { get; set; }
    }
    public class SceneFilter
    {
        [JsonProperty("name_contains")]
        public string Name_Contains { get; set; }
        [JsonProperty("ids")]
        public string[] Ids { get; set; }
    }
    public class SceneFieldsSort
    {
        public const string Name = "name";
    }
    public class ToolSceneViewModel
    {
        [JsonProperty("sceneid")]
        public string SceneId { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("tools")]
        public BorrowToolModel[] Tools { get; set; }

    }

    public class BorrowToolModel
    {
        [JsonProperty("toolid")]
        public string ToolId { get; set; }
        [JsonProperty("borrow-from")]
        public DateTime BorrowFrom { get; set; }
        [JsonProperty("borrow-to")]
        public DateTime BorrowTo { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }

    public class ToolViewContentModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("toolid")]
        public string ToolId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("sceneid")]
        public string SceneId { get; set; }
        [JsonProperty("scene_name")]
        public string SceneName { get; set; }
        [JsonProperty("borrow-from")]
        public DateTime? BorrowFrom { get; set; }
        [JsonProperty("borrow-to")]
        public DateTime? BorrowTo { get; set; }
        [JsonProperty("amount")]
        public int? Amount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class CharacterCreateModel
    {
        [JsonProperty("sceneid")]
        public string SceneId { get; set; }
        [JsonProperty("actorid")]
        public string ActorId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("character")]
        public string Character { get; set; }
        [JsonProperty("script-link")]
        public string ScriptLink { get; set; }
    }
    public class CharacterOfSceneModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("sceneid")]
        public string SceneId { get; set; }
        [JsonProperty("actorid")]
        public string ActorId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("character")]
        public string Character { get; set; }
        [JsonProperty("actor-name")]
        public string ActorName { get; set; }
        [JsonProperty("scene-name")]
        public string SceneName { get; set; }
        [JsonProperty("actor-image")]
        public string ActorImage { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("script-link")]
        public string ScriptLink { get; set; }
    }
    public class ToolDateTime
    {
        [JsonProperty("borrow-from")]
        public DateTime BorrowFrom { get; set; }
        [JsonProperty("borrow-to")]
        public DateTime BorrowTo { get; set; }
    }


}
