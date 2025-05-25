using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
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
            var tasks = _serviceManager.Task.GetTasks(trackChanges: false);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTask(int id)
        {
            var task = _serviceManager.Task.GetTask(id, trackChanges: false);
            return Ok(task);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTasksByUserRole(int userId, [FromQuery] TaskRoles role)
        {
            var tasks = _serviceManager.Task.GetTasksByUserRole(userId, role, trackChanges: false);
            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult CreateTask([FromBody] TaskForManipulationDTO task)
        {
            if (task == null)
            {
                return BadRequest("Task is null");
            }
            var taskDB = _serviceManager.Task.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = taskDB.TaskId }, taskDB);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult UpdateTask(int id, [FromBody] TaskForManipulationDTO task)
        {
            if (task == null)
            {
                return BadRequest("Task is null");
            }
            _serviceManager.Task.UpdateTask(id, task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult DeleteTask(int id)
        {
            _serviceManager.Task.DeleteTask(id);
            return NoContent();
        }

        [HttpPatch("{id}/assign")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult AssignTask(int id, [FromBody] AssignmentModel assignment) // TODO: Think about using Query instead of Body
        {
            _serviceManager.Task.AssignTaskToUser(id, assignment.UserId, assignment.UserRole);
            return NoContent();
        }

    }
}
