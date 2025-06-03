using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.Shared;
using TaskManager.Web.Models.AuthModels;
using TaskManager.Web.Models.Common;

namespace TaskManager.Web.Interfaces;

public interface ITaskManagerClient
{
    Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request);
    Task<ApiResponse<UserProfile>> GetCurrentUserAsync();
    Task<ApiResponse<IEnumerable<TaskDTO>>> GetAllTasks();
    Task<ApiResponse<TaskDTO>> GetTask(int taskId);
    Task<ApiResponse<IEnumerable<TaskStatusLogDTO>>> GetTaskLogs(int taskId);
    Task<ApiResponse<IEnumerable<AttachmentDTO>>> GetTaskAttachments(int taskId);
    Task<ApiResponse<Dictionary<string, string>>> GetUsernamesByIds(IEnumerable<int> ids);
    Task<ApiResponse<Stream>> GetAttachmentFile(int taskId, int attachmentId);

    Task LogoutAsync();
}
