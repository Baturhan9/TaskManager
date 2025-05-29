using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Models
{
    public class LoginDTO
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
