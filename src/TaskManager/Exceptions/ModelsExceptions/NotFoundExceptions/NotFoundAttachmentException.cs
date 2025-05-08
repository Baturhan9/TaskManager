namespace TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions
{
    public class NotFoundAttachmentException : NotFoundException
    {
        public NotFoundAttachmentException(int attachmentId)
            : base($"Attachment with id {attachmentId} not found.") { }
    }
}
