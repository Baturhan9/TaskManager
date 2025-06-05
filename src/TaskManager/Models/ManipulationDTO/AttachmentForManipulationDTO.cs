namespace TaskManager.Models.ManipulationDTO
{
    public class AttachmentForManipulationDTO
    {
        public int TaskId { get; set; }
        public required string FilePath { get; set; }
    }
}
