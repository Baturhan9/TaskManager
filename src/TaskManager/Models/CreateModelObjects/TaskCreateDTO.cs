namespace TaskManager.Models.CreateModelObjects
{
    public class TaskCreateDTO
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public DateTime? DeadLine { get; init; }
        public int? AuthorId { get; init; }
        public int? ReviewerId { get; init; }
        public int? TesterId { get; init; }
        public int? AssignmentId { get; init; }
        public int? ProjectId { get; init; }
    }
}
