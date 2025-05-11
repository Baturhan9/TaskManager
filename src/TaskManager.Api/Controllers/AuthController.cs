using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Models;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IServiceManager _serviceManager;

        public AuthController(ILogger<AuthController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var user = _serviceManager.User.GetUserByEmailAndPassword(
                loginDto.Email,
                loginDto.Password,
                trackChanges: false
            );

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var jwt = new JwtSecurityToken(
                issuer: "Jwt:Issuer",
                audience: "Jwt:Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key")),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new { userId = user.UserId, accessToken = encodedJwt };

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserForManipulationDTO user)
        {
            _serviceManager.User.CreateUser(user);
            return CreatedAtAction(nameof(Register), user);
        }
    }
}
