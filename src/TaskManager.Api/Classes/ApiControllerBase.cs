using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Classes
{
    public class ApiControllerBase : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("User ID is invalid or missing.");
            }
            return userId;
        }
    }
}