using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult GetUserAccesses()
        {
            var userAccesses = _serviceManager.UserAccess.GetUserAccesses(trackChanges: false);
            return Ok(userAccesses);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserAccess(int id)
        {
            var userAccess = _serviceManager.UserAccess.GetUserAccess(id, trackChanges: false);
            return Ok(userAccess);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserAccessByUserId(int userId)
        {
            var userAccess = _serviceManager.UserAccess.GetUserAccessesByUserId(
                userId,
                trackChanges: false
            );
            return Ok(userAccess);
        }

        [HttpPost]
        public IActionResult CreateUserAccess([FromBody] UserAccessForManipulationDTO userAccess)
        {
            if (userAccess == null)
            {
                return BadRequest("UserAccess is null");
            }
            _serviceManager.UserAccess.CreateUserAccess(userAccess);
            return CreatedAtAction(nameof(GetUserAccess), userAccess);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserAccess(
            int id,
            [FromBody] UserAccessForManipulationDTO userAccess
        )
        {
            if (userAccess == null)
            {
                return BadRequest("UserAccess is null");
            }
            _serviceManager.UserAccess.UpdateUserAccess(id, userAccess);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserAccess(int id)
        {
            _serviceManager.UserAccess.DeleteUserAccess(id);
            return NoContent();
        }
    }
}
