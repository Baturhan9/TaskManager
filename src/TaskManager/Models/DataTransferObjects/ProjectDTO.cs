namespace TaskManager.Models.DataTransferObjects
{
    public class ProjectDTO
    {
        public int ProjectId { get; init; }
        public string ShortName { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public string? Description { get; init; }
        public DateTime? DateOfCreate { get; init; }
    }
}
