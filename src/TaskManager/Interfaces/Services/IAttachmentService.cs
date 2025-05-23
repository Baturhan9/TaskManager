using TaskManager.Models.CreateModelObjects;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface IAttachmentService
    {
        IEnumerable<AttachmentDTO> GetAttachments(bool trackChanges);
        IEnumerable<AttachmentDTO> GetAttachmentsByTaskId(int taskId, bool trackChanges);
        AttachmentDTO GetAttachment(int attachmentId, bool trackChanges);
        void CreateAttachment(AttachmentCreateDTO attachment);
        void UpdateAttachment(int attachmentId, AttachmentCreateDTO attachment);
        void DeleteAttachment(int attachmentId);
    }
}
