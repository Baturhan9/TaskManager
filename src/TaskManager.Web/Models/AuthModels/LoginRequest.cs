using System.ComponentModel.DataAnnotations;

namespace TaskManager.Web.Models.AuthModels;

public class LoginRequest
{
    [Required(ErrorMessage = "Email обязателен.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Пароль обязателен.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}