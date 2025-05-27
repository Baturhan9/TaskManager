using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Classes.Utilities;
using TaskManager.Api.Models;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly IConfiguration _configuration;
        private readonly PasswordHelper _passwordHelper;

        public AuthController(
            ILogger<AuthController> logger,
            IServiceManager serviceManager,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _configuration = configuration;
            _passwordHelper = new PasswordHelper();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var user = _serviceManager.User.GetUserByEmail(loginDto.Email, trackChanges: false);

            if (user is null)
                return Unauthorized("Invalid login or password");

            bool isPasswordValid = _passwordHelper.VerifyPassword(user.Password, loginDto.Password);
            if (!isPasswordValid)
                return Unauthorized("Invalid login or password");

            string encodedJwt = GenerateToken(user);

            var response = new { userId = user.UserId, accessToken = encodedJwt };

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO user) //TODO create normal registerDto
        {
            string hashPassword = _passwordHelper.HashPassword(user.ConfirmPassword);
            var userDTO = new UserForManipulationDTO()
            {
                Username = user.Username,
                Email = user.Email,
                Password = hashPassword,
                Role = UserRoles.Developer,
            };
            _serviceManager.User.CreateUser(userDTO);
            return CreatedAtAction(nameof(Register), user);
        }

        private string GenerateToken(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var jwt = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>(SystemEnvironments.JWT_ISSUER),
                audience: _configuration.GetValue<string>(SystemEnvironments.JWT_AUDIENCE),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            _configuration.GetValue<string>(SystemEnvironments.JWT_KEY)
                        )
                    ),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
