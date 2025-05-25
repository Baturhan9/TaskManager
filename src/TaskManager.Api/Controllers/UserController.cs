using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IServiceManager _serviceManager;

        public UserController(ILogger<UserController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult GetUsers()
        {
            var users = _serviceManager.User.GetUsers(trackChanges: false);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult GetUser(int id)
        {
            var user = _serviceManager.User.GetUser(id, trackChanges: false);
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult CreateUser([FromBody] UserForManipulationDTO user)
        {
            if (user == null)
            {
                return BadRequest("User is null");
            }
            var userDB = _serviceManager.User.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = userDB.UserId }, userDB);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult UpdateUser(int id, [FromBody] UserForManipulationDTO user)
        {
            if (user == null)
            {
                return BadRequest("User is null");
            }
            _serviceManager.User.UpdateUser(id, user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult DeleteUser(int id)
        {
            var user = _serviceManager.User.GetUser(id, trackChanges: false);
            if (user == null)
            {
                return NotFound();
            }
            _serviceManager.User.DeleteUser(id);
            return NoContent();
        }
    }
}
