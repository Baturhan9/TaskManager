using System.ComponentModel.DataAnnotations;
using TaskManager.Consts;

namespace TaskManager.Models.ManipulationDTO
{
    public class TaskStatusLogForManipulationDTO
    {
        public int? TaskId { get; set; }
        public int? UserId { get; set; }

        [MinLength(3)]
        [MaxLength(300)]
        public string? Comment { get; set; }
        public string Status { get; set; } = TaskStatuses.Empty.ToString();
        public DateTime? DateUpdate { get; set; } = DateTime.UtcNow;
    }
}
