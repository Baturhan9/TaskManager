namespace TaskManager.Models.ManipulationDTO
{
    public class ProjectForManipulationDTO
    {
        public string ShortName { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public string? Description { get; init; }
        public DateTime DateOfCreate { get; init; } = DateTime.UtcNow;
    }
}
