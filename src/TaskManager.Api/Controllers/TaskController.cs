using Microsoft.AspNetCore.Mvc;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskDTO task)
        {
            if (task == null)
            {
                return BadRequest("Task is null");
            }
            _serviceManager.Task.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskDTO task)
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
        public IActionResult AssignTask(int id, [FromBody] int userId, [FromBody] TaskRoles role)
        {
            _serviceManager.Task.AssignTaskToUser(id, userId, role);
            return NoContent();
        }
    }
}
