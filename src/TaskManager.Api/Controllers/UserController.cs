using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public IActionResult GetUsers()
        {
            var users = _serviceManager.User.GetUsers(trackChanges: false);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _serviceManager.User.GetUser(id, trackChanges: false);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserForManipulationDTO user)
        {
            if (user == null)
            {
                return BadRequest("User is null");
            }
            _serviceManager.User.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), user);
        }

        [HttpPut("{id}")]
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
