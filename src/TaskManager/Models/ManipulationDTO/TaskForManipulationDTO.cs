using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.ManipulationDTO
{
    public class TaskForManipulationDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Title { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]
        public DateTime? DeadLine { get; set; } = DateTime.UtcNow;
        [Required]
        public int? AuthorId { get; set; }
        public int? ReviewerId { get; set; }
        public int? TesterId { get; set; }
        public int? AssignmentId { get; set; }
        [Required]
        public int? ProjectId { get; set; }
    }
}
