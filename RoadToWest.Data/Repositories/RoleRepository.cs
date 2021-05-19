using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Repositories
{
    public partial interface IRoleRepository
    {
        Roles FindById(string Id);
        List<RoleViewModel> FindRolesByUser(string userId);
    }
    public partial class RoleRepository : BaseRepository<Roles, string>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }

        public Roles FindById(string Id)
        {
            return Get().FirstOrDefault(r => r.Id == Id);
        }

        public List<RoleViewModel> FindRolesByUser(string userId)
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            var roles = Get().Where(r => r.UserRole.Any(u => u.UserId == userId));
            foreach (Roles role in roles)
            {
                RoleViewModel view = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };
                list.Add(view);
            }
            return list;
        }
    }
}
