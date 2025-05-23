using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IAttachmentService
    {
        IEnumerable<AttachmentDTO> GetAttachments(bool trackChanges);
        IEnumerable<AttachmentDTO> GetAttachmentsByTaskId(int taskId, bool trackChanges);
        AttachmentDTO GetAttachment(int attachmentId, bool trackChanges);
        AttachmentDTO CreateAttachment(AttachmentForManipulationDTO attachment);
        void UpdateAttachment(int attachmentId, AttachmentForManipulationDTO attachment);
        void DeleteAttachment(int attachmentId);
    }
}
