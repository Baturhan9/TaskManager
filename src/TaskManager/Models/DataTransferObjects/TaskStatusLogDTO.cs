namespace TaskManager.Models.DataTransferObjects
{
    public class TaskStatusLogDTO
    {
        public int TaskStatusId { get; init; }
        public int? TaskId { get; init; }
        public int? UserId { get; init; }
        public string? Comment { get; init; }
        public string Status { get; init; } = string.Empty;
        public DateTime? DateUpdate { get; init; } = DateTime.UtcNow;
    }
}
