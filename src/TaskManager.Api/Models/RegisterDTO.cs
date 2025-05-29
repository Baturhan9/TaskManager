// register model
using System.ComponentModel.DataAnnotations;
using TaskManager.ValidationsAttribute;

namespace TaskManager.Api.Models
{
    public class RegisterDTO
    {
        [MinLength(3)]
        public required string Username { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [StrongPassword]
        public required string Password { get; set; }
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
    }
}
