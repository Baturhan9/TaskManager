using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/tasks/{taskId}/logs")]
    [ApiController]
    [Authorize]
    public class TaskStatusLogController : ControllerBase
    {
        private readonly ILogger<TaskStatusLogController> _logger;
        private readonly IServiceManager _serviceManager;

        public TaskStatusLogController(
            ILogger<TaskStatusLogController> logger,
            IServiceManager serviceManager
        )
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTaskStatusLogs(int taskId)
        {
            var taskStatusLogs = _serviceManager.TaskStatusLog.GetTaskStatusLogs(
                taskId,
                trackChanges: false
            );
            return Ok(taskStatusLogs);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTaskStatusLog(int taskId, int id)
        {
            var taskStatusLog = _serviceManager.TaskStatusLog.GetTaskStatusLog(
                taskId,
                id,
                trackChanges: false
            );
            return Ok(taskStatusLog);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult CreateTaskStatusLog(
            int taskId,
            [FromBody] TaskStatusLogForManipulationDTO taskStatusLog
        )
        {
            if (taskStatusLog is null)
            {
                return BadRequest("TaskStatusLog is null");
            }
            var taskStatusLogDB = _serviceManager.TaskStatusLog.CreateTaskStatusLog(
                taskId,
                taskStatusLog
            );
            return CreatedAtAction(
                nameof(GetTaskStatusLog),
                new { id = taskStatusLogDB.TaskStatusLogId },
                taskStatusLogDB
            );
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult UpdateTaskStatusLog(
            int taskId,
            int id,
            [FromBody] TaskStatusLogForManipulationDTO taskStatusLog
        )
        {
            if (taskStatusLog is null)
            {
                return BadRequest("TaskStatusLog is null");
            }
            _serviceManager.TaskStatusLog.UpdateTaskStatusLog(taskId, id, taskStatusLog);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult DeleteTaskStatusLog(int taskId, int id)
        {
            _serviceManager.TaskStatusLog.DeleteTaskStatusLog(taskId, id);
            return NoContent();
        }
    }
}
