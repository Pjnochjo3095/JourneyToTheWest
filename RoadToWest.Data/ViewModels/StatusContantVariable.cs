using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.ViewModels
{
    public class SceneStatus
    {
        public const string NEW = "New";
        public const string PROCESSING = "Processing";
        public const string DONE = "Done";
        public const string DELETE = "Delete";
    }
    public class ToolStatus
    {
        public const string OUTOFSTOCK = "OutOfStock";
        public const string AVAILABLE = "Available";
        public const string DELETED = "Deleted";
    }
    public class UserStatus
    {
        public const string ENABLE = "Enable";
        public const string DISABLE = "Disable";
    }
    public class CharacterStatus
    {
        public const string ENABLE = "Enable";
        public const string DISABLE = "Disable";
    }
    public class OrderStatus
    {
        public const string NEW = "New";
        public const string DELETED = "Deleted";
    }
    public class ApiDetail
    {
        public const string ROOT_API = "http://192.168.1.160:52833";
        
    }

}
