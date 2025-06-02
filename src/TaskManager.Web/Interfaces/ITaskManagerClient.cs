using TaskManager.Models.Shared;
using TaskManager.Web.Models.AuthModels;
using TaskManager.Web.Models.Common;

namespace TaskManager.Web.Interfaces;

public interface ITaskManagerClient
{
    Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request);
    Task<ApiResponse<UserProfile>> GetCurrentUserAsync();
    Task LogoutAsync();
}
