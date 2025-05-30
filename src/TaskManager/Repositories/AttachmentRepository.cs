using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(TaskManagerContext context)
            : base(context) { }

        public void CreateAttachment(Attachment attachment) => Create(attachment);

        public void DeleteAttachment(Attachment attachment) => Delete(attachment);

        public Attachment GetAttachment(int taskId, int attachmentId, bool trackChanges) =>
            FindByCondition(
                    a => a.AttachmentId.Equals(attachmentId) && a.TaskId.Equals(taskId),
                    trackChanges
                )
                .SingleOrDefault();

        public IEnumerable<Attachment> GetAttachments(int taskId, bool trackChanges) =>
            FindByCondition(a => a.TaskId.Equals(taskId), trackChanges)
                .OrderBy(a => a.AttachmentId)
                .ToList();
    }
}
