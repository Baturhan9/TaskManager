using System.ComponentModel.DataAnnotations;
using TaskManager.ValidationsAttribute;

namespace TaskManager.Models.ManipulationDTO
{
    public class UserForManipulationDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StrongPassword]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}