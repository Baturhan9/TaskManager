using System.Net.Http.Headers;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;
using TaskManager.Models.Shared;
using TaskManager.Web.Interfaces;
using TaskManager.Web.Models.AuthModels;
using TaskManager.Web.Models.Common;

namespace TaskManager.Web.Clients;

public class TaskManagerClient : ITaskManagerClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaskManagerClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    private void SetAuthorizationHeader()
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt_token"];
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );
        }
    }

    private string GetUserIdFromClaims()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.Request.Cookies["user_id"];
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User ID claim is missing.");
        }
        return userIdClaim;
    }

    public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
        var result = await HandleResponse<AuthResponse>(response);

        if (result.Success && result.Data?.Token != null)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(
                "jwt_token",
                result.Data.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddDays(1),
                }
            );
            _httpContextAccessor.HttpContext.Response.Cookies.Append(
                "user_id",
                result.Data.UserId.ToString(),
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddDays(1),
                }
            );
        }

        return result;
    }

    public Task LogoutAsync()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("jwt_token");
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("user_id");
        return Task.CompletedTask;
    }

    public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
        return await HandleResponse<AuthResponse>(response);
    }

    public async Task<ApiResponse<UserProfile>> GetCurrentUserAsync()
    {
        SetAuthorizationHeader();
        var userId = GetUserIdFromClaims();
        var response = await _httpClient.GetAsync($"api/users/{userId}/profile");
        var result = await HandleResponse<UserProfile>(response);
        return result;
    }

    private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<T>();
            return new ApiResponse<T> { Success = true, Data = data };
        }

        var error = await response.Content.ReadAsStringAsync();
        return new ApiResponse<T>
        {
            Success = false,
            ErrorMessage = $"Error {(int)response.StatusCode}: {error}",
        };
    }

    public async Task<ApiResponse<IEnumerable<TaskDTO>>> GetAllTasks()
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync("api/tasks");
        var result = await HandleResponse<IEnumerable<TaskDTO>>(response);
        return result;
    }

    public async Task<ApiResponse<TaskDTO>> GetTask(int taskId)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync($"api/tasks/{taskId}");
        var result = await HandleResponse<TaskDTO>(response);
        return result;
    }

    public async Task<ApiResponse<IEnumerable<TaskStatusLogDTO>>> GetTaskLogs(int taskId)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync($"api/tasks/{taskId}/logs");
        var result = await HandleResponse<IEnumerable<TaskStatusLogDTO>>(response);
        return result;
    }

    public async Task<ApiResponse<IEnumerable<AttachmentDTO>>> GetTaskAttachments(int taskId)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync($"api/tasks/{taskId}/attachments");
        var result = await HandleResponse<IEnumerable<AttachmentDTO>>(response);
        return result;
    }
    public async Task<ApiResponse<Stream>> GetAttachmentFile(int taskId, int attachmentId)
    {
        SetAuthorizationHeader();

        var response = await _httpClient.GetAsync($"api/tasks/{taskId}/attachments/{attachmentId}");
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return new ApiResponse<Stream>
            {
                Success = false,
                ErrorMessage = $"Error {(int)response.StatusCode}: {error}",
            };
        }
        var stream = await response.Content.ReadAsStreamAsync();

        return new ApiResponse<Stream> { Success = true, Data = stream };
    }

    public async Task<ApiResponse<TaskDTO>> CreateTaskAsync(TaskForManipulationDTO dto)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync($"api/tasks", dto);
        var result = await HandleResponse<TaskDTO>(response);
        return result;
    }

    public async Task<ApiResponse<Dictionary<string, UserListDto>>> GetUsersDto()
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync("api/users/all");
        var result = await HandleResponse<Dictionary<string, UserListDto>>(response);
        return result;
    }

    public async Task<ApiResponse<IEnumerable<ProjectDTO>>> GetProjects()
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync("api/projects");
        var result = await HandleResponse<IEnumerable<ProjectDTO>>(response);
        return result;
    }
}
