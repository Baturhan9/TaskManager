namespace TaskManager.Models.ManipulationDTO
{
    public class AttachmentForManipulationDTO
    {
        public int TaskId { get; init; }
        public required string FilePath { get; init; }
    }
}
