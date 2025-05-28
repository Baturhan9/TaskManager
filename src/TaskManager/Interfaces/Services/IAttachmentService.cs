using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IAttachmentService
    {
        IEnumerable<AttachmentDTO> GetAttachments(int taskId, int userId, bool trackChanges);
        AttachmentDTO GetAttachment(int taskId, int attachmentId, int userId, bool trackChanges);
        AttachmentDTO CreateAttachment(
            int taskId,
            int userId,
            AttachmentForManipulationDTO attachment
        );
        void UpdateAttachment(
            int taskId,
            int attachmentId,
            int userId,
            AttachmentForManipulationDTO attachment
        );
        string DeleteAttachment(int taskId, int attachmentId, int userId);
    }
}
