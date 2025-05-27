using Microsoft.AspNetCore.Identity;

namespace TaskManager.Api.Classes.Utilities
{
    public class PasswordHelper
    {
        private readonly PasswordHasher<IdentityUser> _hasher = new PasswordHasher<IdentityUser>();

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}