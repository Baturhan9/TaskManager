using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces.Services;
using TaskManager.Models.CreateModelObjects;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IServiceManager _serviceManager;

        public ProjectController(ILogger<ProjectController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _serviceManager.Project.GetProjects(trackChanges: false);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            var project = _serviceManager.Project.GetProject(id, trackChanges: false);
            return Ok(project);
        }

        [HttpGet("{id}/tasks")]
        public IActionResult GetTasksByProjectId(int id)
        {
            var tasks = _serviceManager.Task.GetTasksByProjectId(id, trackChanges: false);
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectCreateDTO project)
        {
            if (project == null)
            {
                return BadRequest("Project is null");
            }
            _serviceManager.Project.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), project);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] ProjectCreateDTO project)
        {
            if (project == null)
            {
                return BadRequest("Project is null");
            }
            _serviceManager.Project.UpdateProject(id, project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            _serviceManager.Project.DeleteProject(id);
            return NoContent();
        }
    }
}
