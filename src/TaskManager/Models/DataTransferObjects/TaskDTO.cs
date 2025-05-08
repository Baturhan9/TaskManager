namespace TaskManager.Models.DataTransferObjects
{
    public class TaskDTO
    {
        public int TaskId { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public DateTime? Deadline { get; init; }
        public int? AuthorId { get; init; }
        public int? ReviewerId { get; init; }
        public int? TesterId { get; init; }
        public int? AssignmentId { get; init; }
        public int? ProjectId { get; init; }
    }
}
