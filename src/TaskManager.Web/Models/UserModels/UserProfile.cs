namespace TaskManager.Web.Models.UserModels;

public class UserProfile
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}