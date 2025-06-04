using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Classes;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/tasks/{taskId}/logs")]
    [ApiController]
    [Authorize]
    public class TaskStatusLogController : ApiControllerBase
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
            var userId = GetCurrentUserId();
            var taskStatusLogs = _serviceManager.TaskStatusLog.GetTaskStatusLogs(
                taskId,
                userId,
                trackChanges: false
            );
            return Ok(taskStatusLogs);
        }

        [HttpGet("lastStatus")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetLastTaskStatus(int taskId)
        {
            var userId = GetCurrentUserId();
            var status = _serviceManager.TaskStatusLog.GetLastStatusLog(
                taskId,
                userId,
                trackChanges: false
            );
            return Ok(status);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTaskStatusLog(int taskId, int id)
        {
            var userId = GetCurrentUserId();
            var taskStatusLog = _serviceManager.TaskStatusLog.GetTaskStatusLog(
                taskId,
                id,
                userId,
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
            var userId = GetCurrentUserId();
            if (taskStatusLog is null)
            {
                return BadRequest("TaskStatusLog is null");
            }
            var taskStatusLogDB = _serviceManager.TaskStatusLog.CreateTaskStatusLog(
                taskId,
                userId,
                taskStatusLog
            );
            return CreatedAtAction(
                nameof(GetTaskStatusLog),
                new
                {
                    taskId,
                    id = taskStatusLogDB.TaskStatusId,
                    userId,
                },
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
            var userId = GetCurrentUserId();
            if (taskStatusLog is null)
            {
                return BadRequest("TaskStatusLog is null");
            }
            _serviceManager.TaskStatusLog.UpdateTaskStatusLog(taskId, id, userId, taskStatusLog);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult DeleteTaskStatusLog(int taskId, int id)
        {
            var userId = GetCurrentUserId();
            _serviceManager.TaskStatusLog.DeleteTaskStatusLog(taskId, id, userId);
            return NoContent();
        }
    }
}
