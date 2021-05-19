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
    [Route("api/tool")]
    [ApiController]

    public class ToolController : ControllerBase
    {
        private ToolDomain _toolDomain;
        private DbContext context;
        public ToolController(ToolDomain ToolDomain, DbContext context)
        {
            this._toolDomain = ToolDomain;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] string sort, [FromQuery] ToolFilter filter)
        {
            try
            {
                var result = _toolDomain.GetTool(filter, sort);
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
        [HttpGet("get-tool-by-id")]
        public IActionResult GetToolById([FromQuery] string toolId)
        {
            try
            {
                var result = _toolDomain.GetToolById(toolId);
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
        [HttpGet("borrow-tool")]
        public IActionResult BorrowTool(string toolId, int amount)
        {
            try
            {
                var result = _toolDomain.BorrowTool(toolId, amount);
                if (result)
                {
                    return Ok();
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

        [HttpPost]
        public async Task<IActionResult> CreateTool([FromForm]ToolCreateModel model)
        {
            try
            {

                    var result = _toolDomain.CreateTool(model, model.Image);
                    if(result != null)
                    {
                        context.SaveChanges();
                        return Ok(result.Id);
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
        [HttpPut]
        public async Task<IActionResult> UpdateTool([FromForm] ToolUpdateModel model, string toolId)
        {
            try
            {
                
                    var result = _toolDomain.Update(toolId, model, model.Image);
                    if (result != null)
                    {
                        context.SaveChanges();
                        return Ok(result.Id);
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
        [HttpDelete]
        public IActionResult Delete([FromQuery] string toolId)
        {
            try
            {
                Tool deleteTool = _toolDomain.Delete(toolId);
                if (deleteTool != null)
                {
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
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
            buffer = System.IO.File.ReadAllBytes($"images\\tool\\{name}");
            return File(buffer, "image/png");
        }
    }
}
