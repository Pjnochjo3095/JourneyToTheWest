using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.ViewModels
{
    public class UserCreateModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class UserLoginModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class UserViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }
    public class CurrentUserModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("status")]
        public String Status { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("isActor")]
        public bool? IsActor { get; set; }
    }


}
