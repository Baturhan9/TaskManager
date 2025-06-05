using System.ComponentModel.DataAnnotations;
using TaskManager.Consts;

namespace TaskManager.Models.ManipulationDTO
{
    public class TaskStatusLogForManipulationDTO
    {
        public int? TaskId { get; init; }
        public int? UserId { get; init; }

        [MinLength(3)]
        [MaxLength(300)]
        public string? Comment { get; init; }
        public string Status { get; init; } = TaskStatuses.Empty.ToString();
        public DateTimeOffset? DateUpdate { get; init; } = DateTime.UtcNow;
    }
}
