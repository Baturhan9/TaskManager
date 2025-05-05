using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TaskManager.Interfaces.Repositories
{
    public interface IAttachmentRepository
    {
        IEnumerable<Attachment> GetAttachments(bool trackChanges);
        IEnumerable<Attachment> GetAttachmentsByTaskId(int AttachmentId, bool trackChanges);
        Attachment GetAttachment(int taskId, bool trackChanges);
        void DeleteAttachment(Attachment task);
        void CreateAttachment(Attachment task);
    }
}