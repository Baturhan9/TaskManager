using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/useraccesses/")]
    [ApiController]
    [Authorize]
    public class UserAccessController : ControllerBase
    {
        private readonly ILogger<UserAccessController> _logger;
        private readonly IServiceManager _serviceManager;

        public UserAccessController(
            ILogger<UserAccessController> logger,
            IServiceManager serviceManager
        )
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult GetUserAccesses()
        {
            var userAccesses = _serviceManager.UserAccess.GetUserAccesses(trackChanges: false);
            return Ok(userAccesses);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult GetUserAccess(int id)
        {
            var userAccess = _serviceManager.UserAccess.GetUserAccess(id, trackChanges: false);
            return Ok(userAccess);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetUserAccessByUserId(int userId)
        {
            var userAccess = _serviceManager.UserAccess.GetUserAccessesByUserId(
                userId,
                trackChanges: false
            );
            return Ok(userAccess);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult CreateUserAccess([FromBody] UserAccessForManipulationDTO userAccess)
        {
            if (userAccess is null)
            {
                return BadRequest("UserAccess is null");
            }
            var userAccessDB = _serviceManager.UserAccess.CreateUserAccess(userAccess);
            return CreatedAtAction(
                nameof(GetUserAccess),
                new { id = userAccessDB.UserAccessId },
                userAccessDB
            );
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult UpdateUserAccess(
            int id,
            [FromBody] UserAccessForManipulationDTO userAccess
        )
        {
            if (userAccess is null)
            {
                return BadRequest("UserAccess is null");
            }
            _serviceManager.UserAccess.UpdateUserAccess(id, userAccess);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult DeleteUserAccess(int id)
        {
            _serviceManager.UserAccess.DeleteUserAccess(id);
            return NoContent();
        }
    }
}
