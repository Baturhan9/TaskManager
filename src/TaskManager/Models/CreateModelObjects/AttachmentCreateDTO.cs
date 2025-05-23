namespace TaskManager.Models.CreateModelObjects
{
    public class AttachmentCreateDTO
    {
        public int TaskId { get; init; }
        public required string FilePath { get; init; }
    }
}
