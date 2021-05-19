using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Domains;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoadToWest.Api.Controllers
{
    [Route("api/scene")]
    [ApiController]

    public class SceneController : ControllerBase
    {
        private SceneDomain _sceneDomain;
        private DbContext context;
        private OrderToolDomain _orderDomain;
        public SceneController(SceneDomain sceneDomain, DbContext context, OrderToolDomain order)
        {
            this._sceneDomain = sceneDomain;
            this.context = context;
            this._orderDomain = order;
        }
        [HttpPost]
        public IActionResult Post([FromBody] SceneCreateModel model)
        {
            try
            {
                Scene scene = _sceneDomain.Create(model);
                if (scene != null)
                {
                    context.SaveChanges();
                    return Ok(scene.Id);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update([FromQuery] string sceneId, [FromBody] SceneUpdateModel model)
        {
            try
            {
                Scene updateScene = _sceneDomain.Update(sceneId, model);
                if (updateScene != null)
                {
                    context.SaveChanges();
                    return Ok(updateScene.Id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete([FromQuery] string sceneId)
        {
            try
            {
                Scene deleteScene = _sceneDomain.Delete(sceneId);
                if (deleteScene != null)
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
        [HttpGet]
        public IActionResult Get([FromQuery] string sort, [FromQuery] SceneFilter filter)
        {
            try
            {
                var result = _sceneDomain.GetScene(filter, sort);
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
        [HttpGet("get-scene-by-id")]
        public IActionResult GetSceneById([FromQuery] string sceneId)
        {
            try
            {
                var result = _sceneDomain.GetSceneById(sceneId);
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

        [HttpPost("add-tool")]
        public IActionResult AddToolFilm([FromBody] ToolSceneViewModel model)
        {
            try
            {
                var result = _orderDomain.CreateToolScene(model);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-tools-of-scene")]
        public IActionResult GetTools(string sceneId)
        {
            try
            {
                var result = _orderDomain.GetTools(sceneId);
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
        [HttpPost("add-character")]
        public async Task<IActionResult> AddActor([FromBody] CharacterCreateModel model)
        {
            try
            {

                
                    var result = _sceneDomain.AddCharacter(model, model.ScriptLink);
                    if (result)
                    {
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Null");
                    }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("delete-character")]
        public IActionResult DeleteCharacter([FromQuery] string characterId)
        {
            try
            {
                var result = _sceneDomain.DeleteCharacter(characterId);
                if (result)
                {
                    context.SaveChanges();
                    return Ok();
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
        [HttpGet("all-tool")]
        public IActionResult getAllTool([FromQuery] ToolDateTime model)
        {
            try
            {

                var result = _orderDomain.GetToolsByDate(model.BorrowFrom, model.BorrowTo);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Null");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-characters")]
        public IActionResult GetCharacters(string sceneId)
        {
            try
            {

                var result = _sceneDomain.GetCharacters(sceneId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Null");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("get-script-by-id")]
        public IActionResult GetScriptCharacter(string characterId)
        {
            try
            {
                var script = _sceneDomain.GetScript(characterId);
                if (script != null)
                {
                    Byte[] b;
                    b = System.IO.File.ReadAllBytes($"scripts\\character\\{script}.txt");
                    return File(b, "text/plain");
                }
                else
                {
                    return BadRequest("Not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("finish")]
        public IActionResult Finish([FromQuery] string sceneId)
        {
            try
            {
                var result = _sceneDomain.FinishScene(sceneId);
                if (result)
                {
                    context.SaveChanges();
                    return Ok();
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
        [HttpDelete("delete-order")]
        public IActionResult DeleteOrder([FromQuery] string orderId)
        {
            try
            {
                var result = _orderDomain.DeleteOrder(orderId);
                if (result)
                {
                    context.SaveChanges();
                    return Ok();
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
       
    }
}
