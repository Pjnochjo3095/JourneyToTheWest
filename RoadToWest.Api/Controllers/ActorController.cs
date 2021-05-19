using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Domains;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoadToWest.Api.Controllers
{
    [Route("api/actor")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private ActorDomain _actorDomain;
        private DbContext context;
        public ActorController(ActorDomain actorDomain, DbContext context)
        {
            this._actorDomain = actorDomain;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ActorFilter filter,
            [FromQuery] string sort,
            [FromQuery] int page = 0,
            [FromQuery] int limit = -1)
        {
            try
            {
                var result = _actorDomain.GetActor(filter, sort, page, limit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id")]
        public IActionResult GetById(string id)
        {
            try
            {
                var result = _actorDomain.GetById(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-roles")]
        public IActionResult GetRolesById()
        {
            try
            {
                var result = _actorDomain.GetRoles();
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("null");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("image")]
        public IActionResult GetImage(string name)
        {
            Byte[] buffer;
            if (name == null)
            {
                return BadRequest("Not found");
            }
            buffer = System.IO.File.ReadAllBytes($"images\\avatar\\{name}");
            return File(buffer, "image/png");
        }
        [HttpGet("get-character-by-id")]
        public IActionResult GetCharacterOfActor(string actorId)
        {
            try
            {
                var result = _actorDomain.GetCharactorOfActor(actorId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("null");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-scenes")]
        public IActionResult GetScenes(string actorId)
        {
            try
            {
                var result = _actorDomain.GetScenes(actorId);
                if (result.Count >= 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("null");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-scenes-is-finished")]
        public IActionResult GetScenesIsFinished(string actorId)
        {
            try
            {
                var result = _actorDomain.GetScenesIsFinished(actorId);
                if (result.Count >= 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("null");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ActorCreateModel model)
        {
            try 
            {
              
                    var actor = _actorDomain.Create(model, model.Image);
                    if (actor != null)
                    {
                        context.SaveChanges();
                        return Ok(actor.Id);
                    }
                    else
                    {
                        return BadRequest();
                    }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ActorUpdateModel model,string id)
        {
            try
            {
         
                    var actor = _actorDomain.EditActor(id,model, model.Image);
                    if (actor != null)
                    {
                        context.SaveChanges();
                        return Ok(actor);
                    }
                    else
                    {
                        return BadRequest();
                    }
            
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("enable-actor")]
        public IActionResult EnableAndDisableActor([FromQuery] string id)
        {
            try
            {
                var updated = _actorDomain.EnableActor(id);
                if (updated != null)
                {
                    context.SaveChanges();
                    return Ok(true);
                }
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        


    }
}
