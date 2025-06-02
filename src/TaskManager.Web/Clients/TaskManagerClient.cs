using System.Net.Http.Headers;
using TaskManager.Web.Interfaces;
using TaskManager.Web.Models.AuthModels;
using TaskManager.Web.Models.Common;
using TaskManager.Web.Models.UserModels;

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
        }

        return result;
    }

    public Task LogoutAsync()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("jwt_token");
        return Task.CompletedTask;
    }

    public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
        return await HandleResponse<AuthResponse>(response);
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

    public async Task<ApiResponse<UserProfile>> GetCurrentUserAsync()
    {
        SetAuthorizationHeader();
        HandleResponse<UserProfile>(_httpClient.GetAsync("api/user/profile"));
    }
}
