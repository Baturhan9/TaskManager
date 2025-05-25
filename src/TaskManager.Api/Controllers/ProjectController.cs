using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetProjects()
        {
            var projects = _serviceManager.Project.GetProjects(trackChanges: false);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetProject(int id)
        {
            var project = _serviceManager.Project.GetProject(id, trackChanges: false);
            return Ok(project);
        }

        [HttpGet("{id}/tasks")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTasksByProjectId(int id)
        {
            var tasks = _serviceManager.Task.GetTasksByProjectId(id, trackChanges: false);
            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Senior)]
        public IActionResult CreateProject([FromBody] ProjectForManipulationDTO project)
        {
            if (project == null)
            {
                return BadRequest("Project is null");
            }
            var projectDB = _serviceManager.Project.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = projectDB.ProjectId }, projectDB);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Senior)]
        public IActionResult UpdateProject(int id, [FromBody] ProjectForManipulationDTO project)
        {
            if (project == null)
            {
                return BadRequest("Project is null");
            }
            _serviceManager.Project.UpdateProject(id, project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Senior)]
        public IActionResult DeleteProject(int id)
        {
            _serviceManager.Project.DeleteProject(id);
            return NoContent();
        }
    }
}
