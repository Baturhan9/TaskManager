using System.ComponentModel.DataAnnotations;
using TaskManager.ValidationsAttribute;

namespace TaskManager.Web.Models.AuthModels;

public class RegisterRequest
{
    [Required(ErrorMessage = "Username обязателен.")]
    [MinLength(3, ErrorMessage = "Имя пользователя должен состоять минимум из 3 символов")]
    public required string Username { get; set; }
    [Required(ErrorMessage = "Email обязателен.")]
    [EmailAddress(ErrorMessage = "Некорректный формат email.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен.")]
    [DataType(DataType.Password)]
    [StrongPassword]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Пароль обязателен.")]
    [DataType(DataType.Password)]
    [StrongPassword]
    public required string ConfirmPassword { get; set; }
}