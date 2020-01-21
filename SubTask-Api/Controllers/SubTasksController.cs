using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubTask_Api.Interfaces;

namespace SubTask_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubTasksController : ControllerBase
    {
        private readonly IRepositoryFactory _repository;
        private readonly ILogger<SubTasksController> _logger;
        public SubTasksController(IRepositoryFactory repository, ILogger<SubTasksController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        // GET: api/SubTasks
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var subtasks = await _repository.SubTask.GetAllSubTasksAsync();
                return Ok(subtasks);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ExceptionHelper.ErrorDetails(ex));
            }
        }

        // GET: api/SubTasks/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id, [FromRoute] string accept = "json")
        {
            try
            {
                var subtask = await _repository.SubTask.GetSubTaskByIdAsync(id);

                if (subtask == null)
                {
                    _logger.LogError($"task id: {id}, not found.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"found task with id: {id}");
                    if (accept.EndsWith("hal"))
                    {
                        var link = new LinkHelper<Model.SubTask>(subtask);
                        link._links.Add(new Link
                        {
                            Href = Url.Link("Get", new { subtask.Id }),
                            Rel = "self",
                            method = "GET"
                        });
                        link._links.Add(new Link
                        {
                            Href = Url.Link("DeleteSubTask", new { subtask.Id }),
                            Rel = "delete-subtask",
                            method = "DELETE"
                        });
                        return new ObjectResult(link);
                    }
                    return Ok(subtask);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error occured: {ex.Message}");
                return new BadRequestObjectResult(ExceptionHelper.ErrorDetails(ex));
            }
        }

        // POST: api/SubTasks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Model.SubTask subtask)
        {
            try
            {
                if (subtask == null)
                {
                    _logger.LogError("task is null.");
                    return BadRequest("task is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid model task.");
                    return BadRequest("Invalid model object");
                }

                await _repository.SubTask.CreateSubTaskAsync(subtask);

                return CreatedAtRoute("Get", new { id = subtask.Id }, subtask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error occured: {ex.Message}");
                return new BadRequestObjectResult(ExceptionHelper.ErrorDetails(ex));
            }
        }

        //// PUT: api/SubTasks/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "DeleteSubTask")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var task = await _repository.SubTask.GetSubTaskByIdAsync(id);

                if (task == null)
                {
                    _logger.LogError($"task id: {id}, not found.");
                    return NotFound();
                }
                await _repository.SubTask.DeleteSubtaskAsync(task);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error occured: {ex.Message}");
                return new BadRequestObjectResult(ExceptionHelper.ErrorDetails(ex));
            }
        }
    }
}
