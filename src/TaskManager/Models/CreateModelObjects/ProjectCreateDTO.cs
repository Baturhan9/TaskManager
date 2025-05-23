namespace TaskManager.Models.CreateModelObjects
{
    public class ProjectCreateDTO
    {
        public string ShortName { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public string? Description { get; init; }
        public DateTime DateOfCreate { get; init; } = DateTime.UtcNow;
    }
}
