using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.ManipulationDTO
{
    public class ProjectForManipulationDTO
    {
        [MinLength(3)]
        [MaxLength(255)]
        [Required]
        public string ShortName { get; init; } = string.Empty;
        [MinLength(3)]
        [MaxLength(255)]
        [Required]
        public string FullName { get; init; } = string.Empty;
        [MinLength(3)]
        [MaxLength(3000)]
        [Required]
        public string? Description { get; init; }
        public DateTimeOffset DateOfCreate { get; init; } = DateTime.UtcNow;
    }
}
