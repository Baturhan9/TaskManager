namespace TaskManager.Web.Models.AuthModels;

public class AuthResponse
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string Error { get; set; }
}
