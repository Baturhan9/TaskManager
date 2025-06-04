using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;
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
    Task<ApiResponse<Dictionary<string, UserListDto>>> GetUsersDto();
    Task<ApiResponse<Stream>> GetAttachmentFile(int taskId, int attachmentId);
    Task<ApiResponse<TaskDTO>> CreateTaskAsync(TaskForManipulationDTO dto);
    Task<ApiResponse<IEnumerable<ProjectDTO>>> GetProjects();

    Task LogoutAsync();
}
