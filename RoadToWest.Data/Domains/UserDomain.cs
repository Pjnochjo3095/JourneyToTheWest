using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoadToWest.Data.Models;
using RoadToWest.Data.Repositories;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RoadToWest.Data.Domains
{
    public class UserDomain
    {
        private IUserRepository _userRepo;
        private IRoleRepository _roleRepo;
        private IUserRoleRepository _userRoleRepo;
        private DbContext context;
        public UserDomain(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, DbContext dbcontext)
        {
            this._userRepo = userRepository;
            this._roleRepo = roleRepository;
            this._userRoleRepo = userRoleRepository;
            this.context = dbcontext;
        }
        public String Register(UserCreateModel model)
        {
            var newUser = _userRepo.CreateUser(model);
            
            return newUser.Id;
        }
        public string AddRoleAdminToUser(string userId)
        {
            var user = _userRepo.FindById(userId);
            string roles = "2";
           return AddRoleToUser(user, roles);
        }
        public string AddRoleUserToUser(string userId)
        {
            var user = _userRepo.FindById(userId);
            string roles = "1" ;
            return AddRoleToUser(user, roles);
        }
        public string RemoveRoleUser(string userId) 
        {
            var user = _userRepo.FindById(userId);
            if(user != null)
            {
                var role = _userRoleRepo.Get().Where(s => s.RoleId == "1" && s.User.Id == userId).FirstOrDefault();
                if(role != null)
                {
                    _userRoleRepo.Remove(role);
                    return userId;
                }
                return "";
            }
            return "";
        }
        public string RemoveRoleAdmin(string userId)
        {
            var user = _userRepo.FindById(userId);
            if (user != null)
            {
                var role = _userRoleRepo.Get().Where(s => s.RoleId == "2" && s.User.Id == userId).FirstOrDefault();
                if (role != null)
                {
                    _userRoleRepo.Remove(role);
                    return userId;
                }
                return "";
            }
            return "";
        }
        public DbSet<User> Get()
        {
            return _userRepo.Get();
        }
        public User ChangeStatus(string userId, string status)
        {
            var user = _userRepo.Get().Where(s => s.Id == userId).FirstOrDefault();
            if (user != null)
            {
                var newUser = _userRepo.ChangeStatus(user,status);
                return _userRepo.Update(newUser).Entity;
            }
            return null;
        }
        public UserViewModel GetUserById(string userId)
        {
            var user = _userRepo.Get().Where(s => s.Id == userId).FirstOrDefault();
            if (user != null)
            {
                var result = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.Username
                };
                return result;
            }
            return null;
        }
        public string AddRoleToUser(User user, string roleId)
        {

            var currentRole = _userRoleRepo.AddRole(user.Id, roleId);
            return user.Id;

        }
        public UserViewModel Login(UserLoginModel model)
        {
            var user = _userRepo.Get().FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                var result = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.Username
                };
                return result;
            }
            return null;
        }
        private string GetRoleOfUser(string userId)
        {
            var roles = _userRoleRepo.Get().Where(r => r.UserId == userId).Select(x => x.Role.Name).ToList();
            if (roles != null)
            {
                var result = "";
                foreach ( string role in roles) {
                    result += role + ", ";
                }
                return result;
            }
            return null;
        }
        public string GenerateToken(UserViewModel model)
        {
            var handler = new JwtSecurityTokenHandler();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, model.Username));
            claims.Add(new Claim("Id", model.Id));
            var role = GetRoleOfUser(model.Id);
            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
            var key = Encoding.ASCII.GetBytes("PRIVATE_RoadToWest_KEY");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = "RoadToWest_Issue",
                Audience = "RoadToWest_Issue",
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var newToken = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(newToken);
        }
        public CurrentUserModel GetCurrentUserInfo(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var result = handler.ReadJwtToken(token) as JwtSecurityToken;
            var currentClaims = result.Claims.ToList();
            string id = currentClaims.FirstOrDefault(c => c.Type == "Id").Value;
            string role = currentClaims.FirstOrDefault(c => c.Type == "role").Value;
            var user = _userRepo.Get().FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                if(user.Actor != null)
                {
                    var model = new CurrentUserModel
                    {
                        Id = user.Id,
                        Name = user.Actor.Name,
                        Role = role,
                        Image = ApiDetail.ROOT_API + "/api/actor/image?name=" +user.Actor.Image,
                        Username = user.Username,
                        Status = user.Status,
                        IsActor = user.IsActor, 
                    };
                    return model;
                }
                else
                {
                    var model = new CurrentUserModel
                    {
                        Id = user.Id,
                        Role = role,
                        Username = user.Username,
                        Status = user.Status,
                        IsActor = user.IsActor
                    };
                    return model;
                }
            }
            return null;
        }


    }
}
