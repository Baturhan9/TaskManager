using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TaskManager.ValidationsAttribute
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Пароль не может быть пустым.");
            }

            var password = value.ToString();
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSpecialChar = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (password.Length < 8)
            {
                return new ValidationResult("Пароль должен содержать минимум 8 символов.");
            }
            if (!hasLowerChar.IsMatch(password))
            {
                return new ValidationResult("Пароль должен содержать хотя бы одну строчную букву.");
            }
            if (!hasUpperChar.IsMatch(password))
            {
                return new ValidationResult(
                    "Пароль должен содержать хотя бы одну заглавную букву."
                );
            }
            if (!hasNumber.IsMatch(password))
            {
                return new ValidationResult("Пароль должен содержать хотя бы одну цифру.");
            }
            if (!hasSpecialChar.IsMatch(password))
            {
                return new ValidationResult(
                    "Пароль должен содержать хотя бы один специальный символ (!@#$%^&* и т.д.)."
                );
            }

            return ValidationResult.Success;
        }
    }
}
