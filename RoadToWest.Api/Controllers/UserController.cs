using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Domains;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadToWest.Api.Controllers
{
    [Route("api/user")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private UserDomain _userDomain;
        private DbContext context;
        public UserController(UserDomain userDomain, DbContext context)
        {
            this._userDomain = userDomain;
            this.context = context;
        }
        [HttpGet("get-user-by-id")]
        public IActionResult GetUserById([FromQuery] string userId)
        {
            try
            {
                var result = _userDomain.GetUserById(userId);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var result = _userDomain.GetCurrentUserInfo(token);
                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Error");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
           
        [HttpPost]
        public IActionResult Post([FromBody] UserCreateModel model)
        {
            try
            {
                string userId = _userDomain.Register(model);
                if (userId != null)
                {
                    context.SaveChanges();
                    return Ok(userId);
                }
                return BadRequest("Create Fail");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            try
            {
                var currentUser = _userDomain.Login(model);
                if (currentUser != null)
                {
                    var token = _userDomain.GenerateToken(currentUser);
                    return Ok(token);
                }
                return BadRequest("Invalid Username or Password");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("add-role-admin")]
        public IActionResult AddRoleAdmin([FromQuery] string userId)
        {
            try
            {
                string newUser = _userDomain.AddRoleAdminToUser(userId);
                if (newUser != null)
                {
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("add-role-user")]
        public IActionResult AddRoleUser([FromQuery] string userId)
        {
            try
            {
                string newUser = _userDomain.AddRoleUserToUser(userId);
                if (newUser != null)
                {
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("delete-role-admin")]
        public IActionResult DeleteRoleAdmin([FromQuery] string userId)
        {
            try
            {
                string newUser = _userDomain.RemoveRoleAdmin(userId);
                if (newUser != null)
                {
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("delete-role-user")]
        public IActionResult DeleteRoleUser([FromQuery] string userId)
        {
            try
            {
                string newUser = _userDomain.RemoveRoleUser(userId);
                if (newUser != null)
                {
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
