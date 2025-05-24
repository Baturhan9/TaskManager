using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetTasks()
        {
            var tasks = _serviceManager.Task.GetTasks(trackChanges: false);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = _serviceManager.Task.GetTask(id, trackChanges: false);
            return Ok(task);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetTasksByUserRole(int userId, [FromQuery] TaskRoles role)
        {
            var tasks = _serviceManager.Task.GetTasksByUserRole(userId, role, trackChanges: false);
            return Ok(tasks);
        }

        [HttpPost]
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
        public IActionResult DeleteTask(int id)
        {
            _serviceManager.Task.DeleteTask(id);
            return NoContent();
        }

        [HttpPatch("{id}/assign")]
        public IActionResult AssignTask(int id, [FromBody] AssignmentModel assignment) // TODO: Think about using Query instead of Body
        {
            _serviceManager.Task.AssignTaskToUser(id, assignment.UserId, assignment.UserRole);
            return NoContent();
        }

        [HttpPut("{id}/testing")]
        [Authorize(Policy = UserRoles.Tester)]
        public IActionResult TestTask(int id)
        {
            return Ok("The task is tested");
        }

        [HttpPut("{id}/review")]
        [Authorize(Policy = UserRoles.Senior)]
        public IActionResult ReviewTask(int id)
        {
            return Ok("The task is reviewed");
        }

        [HttpPut("{id}/admin")]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult AdminTask(int id)
        {
            return Ok("This is only for admin");
        }

        [HttpPut("{id}/dev")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult JustDev(int id)
        {
            return Ok("this can everyone");
        }


    }
}
