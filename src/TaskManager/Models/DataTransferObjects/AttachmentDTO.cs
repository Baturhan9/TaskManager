namespace TaskManager.Models.DataTransferObjects
{
    public class AttachmentDTO
    {
        public int AttachmentId { get; init; }
        public int? TaskId { get; init; }
        public string FilePath { get; init; } = string.Empty;
    }
}