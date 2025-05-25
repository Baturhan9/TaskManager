using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Classes;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize]
    public class ProjectController : ApiControllerBase
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
            var userId = GetCurrentUserId();
            var projects = _serviceManager.Project.GetProjects(userId, trackChanges: false);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetProject(int id)
        {
            var userId = GetCurrentUserId();
            var project = _serviceManager.Project.GetProject(id, userId, trackChanges: false);
            return Ok(project);
        }

        [HttpGet("{id}/tasks")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetTasksByProjectId(int id)
        {
            var userId = GetCurrentUserId();
            var tasks = _serviceManager.Task.GetTasksByProjectId(id, userId, trackChanges: false);
            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Senior)]
        public IActionResult CreateProject([FromBody] ProjectForManipulationDTO project)
        {
            var userId = GetCurrentUserId();
            if (project is null)
            {
                return BadRequest("Project is null");
            }
            var projectDB = _serviceManager.Project.CreateProject(project, userId);
            return CreatedAtAction(nameof(GetProject), new { id = projectDB.ProjectId }, projectDB);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Senior)]
        public IActionResult UpdateProject(int id, [FromBody] ProjectForManipulationDTO project)
        {
            var userId = GetCurrentUserId();
            if (project is null)
            {
                return BadRequest("Project is null");
            }
            _serviceManager.Project.UpdateProject(id, userId, project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Senior)]
        public IActionResult DeleteProject(int id)
        {
            var userId = GetCurrentUserId();
            _serviceManager.Project.DeleteProject(id, userId);
            return NoContent();
        }
    }
}
