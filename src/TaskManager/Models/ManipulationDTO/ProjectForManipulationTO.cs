using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.ManipulationDTO
{
    public class ProjectForManipulationDTO
    {
        [MinLength(3)]
        [MaxLength(255)]
        [Required]
        public string ShortName { get; set; } = string.Empty;
        [MinLength(3)]
        [MaxLength(255)]
        [Required]
        public string FullName { get; set; } = string.Empty;
        [MinLength(3)]
        [MaxLength(3000)]
        [Required]
        public string? Description { get; set; }
        public DateTime? DateOfCreate { get; set; } = DateTime.UtcNow;
    }
}
