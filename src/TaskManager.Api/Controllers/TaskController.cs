using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Classes;
using TaskManager.Api.Models;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TaskController : ApiControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IServiceManager _serviceManager;

        public TaskController(ILogger<TaskController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTasks()
        {
            var userId = GetCurrentUserId();
            var tasks = _serviceManager.Task.GetTasks(userId, trackChanges: false);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTask(int id)
        {
            var userId = GetCurrentUserId();
            var task = _serviceManager.Task.GetTask(id, userId, trackChanges: false);
            return Ok(task);
        }

        [HttpGet("byRole")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTasksByUserRole([FromQuery] TaskRoles role)
        {
            var userId = GetCurrentUserId();
            var tasks = _serviceManager.Task.GetTasksByUserRole(userId, role, trackChanges: false);
            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult CreateTask([FromBody] TaskForManipulationDTO task)
        {
            var userId = GetCurrentUserId();
            if (task is null)
            {
                return BadRequest("Task is null");
            }
            var taskDB = _serviceManager.Task.CreateTask(userId, task);
            return CreatedAtAction(nameof(GetTask), new { id = taskDB.TaskId }, taskDB);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult UpdateTask(int id, [FromBody] TaskForManipulationDTO task)
        {
            var userId = GetCurrentUserId();
            if (task is null)
            {
                return BadRequest("Task is null");
            }
            _serviceManager.Task.UpdateTask(id, userId, task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Senior)]
        public IActionResult DeleteTask(int id)
        {
            var userId = GetCurrentUserId();
            _serviceManager.Task.DeleteTask(id, userId);
            return NoContent();
        }

        [HttpPatch("{id}/assign")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult AssignTask(int id, [FromBody] AssignmentModel assignment)
        {
            var userId = GetCurrentUserId();
            _serviceManager.Task.AssignTaskToUser(id, userId, assignment.UserId, assignment.UserRole);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult AssignTask(int id, TaskStatuses status)
        {
            var userId = GetCurrentUserId();
            _serviceManager.Task.ChangeTaskStatus(id, userId, status);
            return NoContent();
        }
    }
}
