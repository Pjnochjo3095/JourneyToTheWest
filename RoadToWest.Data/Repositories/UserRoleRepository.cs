using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Repositories
{
    public partial interface IUserRoleRepository : IBaseRepository<UserRole, string>
    {
        UserRole AddRole(string userId, string roleId);
    }
    public partial class UserRoleRepository : BaseRepository<UserRole, string>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context) : base(context)
        {
        }

        public UserRole AddRole(string userId, string roleId)
        {
            var currentRole = Get().Where(s => s.RoleId == roleId && s.User.Id == userId).FirstOrDefault();
            if(currentRole == null)
            {
                var userRole = new UserRole
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    RoleId = roleId
                };
                var result = Create(userRole).Entity;
                SaveChanges();
                return result;
            }
            return null;

        }
    }

}
