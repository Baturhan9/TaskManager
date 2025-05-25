using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IAttachmentService
    {
        IEnumerable<AttachmentDTO> GetAttachments(int taskId, bool trackChanges);
        AttachmentDTO GetAttachment(int taskId, int attachmentId, bool trackChanges);
        AttachmentDTO CreateAttachment(
            int taskId,
            AttachmentForManipulationDTO attachment
        );
        void UpdateAttachment(
            int taskId,
            int attachmentId,
            AttachmentForManipulationDTO attachment
        );
        void DeleteAttachment(int taskId, int attachmentId);
    }
}
