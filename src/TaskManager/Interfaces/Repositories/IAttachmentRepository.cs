using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IAttachmentRepository
    {
        IEnumerable<Attachment> GetAttachments(int taskId, bool trackChanges);
        Attachment GetAttachment(int taskId, int attachmentId, bool trackChanges);
        void DeleteAttachment(Attachment attachment);
        void CreateAttachment(Attachment attachment);
    }
}
