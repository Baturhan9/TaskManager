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

        public Attachment GetAttachment(int attachmentId, bool trackChanges) =>
            FindByCondition(a => a.AttachmentId.Equals(attachmentId), trackChanges)
                .SingleOrDefault();

        public IEnumerable<Attachment> GetAttachments(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(a => a.AttachmentId).ToList();

        public IEnumerable<Attachment> GetAttachmentsByTaskId(
            int AttachmentId,
            bool trackChanges
        ) => FindByCondition(a => a.TaskId.Equals(AttachmentId), trackChanges).ToList();
    }
}
