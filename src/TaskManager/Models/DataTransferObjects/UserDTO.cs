namespace TaskManager.Models.DataTransferObjects
{
    public class UserDTO
    {
        public int UserId { get; init; }
        public string Username { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
    }
}
