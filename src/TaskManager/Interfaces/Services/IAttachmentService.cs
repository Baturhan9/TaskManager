using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface IAttachmentService
    {
        IEnumerable<AttachmentDTO> GetAttachments(bool trackChanges);
        AttachmentDTO GetAttachment(int attachmentId, bool trackChanges);
        void CreateAttachment(AttachmentDTO attachment);
        void UpdateAttachment(int attachmentId, AttachmentDTO attachment);
        void DeleteAttachment(int attachmentId);
    }
}
