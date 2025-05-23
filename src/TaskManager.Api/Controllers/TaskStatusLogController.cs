using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult GetTaskStatusLogs()
        {
            var taskStatusLogs = _serviceManager.TaskStatusLog.GetTaskStatusLogs(
                trackChanges: false
            );
            return Ok(taskStatusLogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskStatusLog(int id)
        {
            var taskStatusLog = _serviceManager.TaskStatusLog.GetTaskStatusLog(
                id,
                trackChanges: false
            );
            return Ok(taskStatusLog);
        }

        [HttpGet("/task/{taskId}")]
        public IActionResult GetTaskStatusLogByTaskId(int taskId)
        {
            var taskStatusLog = _serviceManager.TaskStatusLog.GetTaskStatusLogsByTaskId(
                taskId,
                trackChanges: false
            );
            return Ok(taskStatusLog);
        }

        [HttpPost]
        public IActionResult CreateTaskStatusLog(
            [FromBody] TaskStatusLogForManipulationDTO taskStatusLog
        )
        {
            if (taskStatusLog == null)
            {
                return BadRequest("TaskStatusLog is null");
            }
            _serviceManager.TaskStatusLog.CreateTaskStatusLog(taskStatusLog);
            return CreatedAtAction(nameof(GetTaskStatusLog), taskStatusLog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTaskStatusLog(
            int id,
            [FromBody] TaskStatusLogForManipulationDTO taskStatusLog
        )
        {
            if (taskStatusLog == null)
            {
                return BadRequest("TaskStatusLog is null");
            }
            _serviceManager.TaskStatusLog.UpdateTaskStatusLog(id, taskStatusLog);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaskStatusLog(int id)
        {
            _serviceManager.TaskStatusLog.DeleteTaskStatusLog(id);
            return NoContent();
        }
    }
}
