using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IAttachmentRepository
    {
        IEnumerable<Attachment> GetAttachments(bool trackChanges);
        IEnumerable<Attachment> GetAttachmentsByTaskId(int AttachmentId, bool trackChanges);
        Attachment GetAttachment(int attachmentId, bool trackChanges);
        void DeleteAttachment(Attachment attachment);
        void CreateAttachment(Attachment attachment);
    }
}
