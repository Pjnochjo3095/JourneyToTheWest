using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Repositories
{
    public partial interface IUserRepository : IBaseRepository<User,string>
    {
        User CreateUser(UserCreateModel model);
        User PrepareCreate(UserCreateModel model);
        User FindById(string id);
        User ChangeStatus(User entity, string status);
    }
    public partial class UserRepository : BaseRepository<User, string>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User CreateUser(UserCreateModel model)
        {
            var user = PrepareCreate(model);
            Create(user);
            SaveChanges();
            return user;
        }

        public User ChangeStatus(User entity, string status)
        {
            if (status.Equals("ENABLE"))
            {
                entity.Status = UserStatus.ENABLE;
            }
            else
            {
                entity.Status = UserStatus.DISABLE;
            }
            return entity;
        }


        public User FindById(string id)
        {
                return Get().FirstOrDefault(u => u.Id == id);
        }

        public User PrepareCreate(UserCreateModel model)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = model.Username,
                Password = model.Password,
                Status = UserStatus.ENABLE,
                IsActor = false
            };  
            return user;
        }
    }
}
