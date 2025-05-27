using TaskManager.Consts;

namespace TaskManager.Models.ManipulationDTO
{
    public class TaskStatusLogForManipulationDTO
    {
        public int? TaskId { get; init; }
        public int? UserId { get; init; }
        public string? Comment { get; init; }
        public string Status { get; init; } = TaskStatuses.Empty.ToString();
        public DateTime? DateUpdate { get; init; } = DateTime.UtcNow;
    }
}
