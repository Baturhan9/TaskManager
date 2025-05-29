using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.ManipulationDTO
{
    public class TaskForManipulationDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Title { get; init; }
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Description { get; init; }
        public DateTime? DeadLine { get; init; }
        public int? AuthorId { get; init; }
        public int? ReviewerId { get; init; }
        public int? TesterId { get; init; }
        public int? AssignmentId { get; init; }
        public int? ProjectId { get; init; }
    }
}
