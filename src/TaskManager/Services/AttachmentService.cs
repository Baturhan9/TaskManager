using AutoMapper;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AttachmentService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void CreateAttachment(AttachmentDTO attachment)
        {
            var attachmentDB = _mapper.Map<Attachment>(attachment);
            _repositoryManager.Attachment.CreateAttachment(attachmentDB);
            _repositoryManager.Save();
        }

        public void DeleteAttachment(int attachmentId)
        {
            var attachment = _repositoryManager.Attachment.GetAttachment(
                attachmentId,
                trackChanges: false
            );
            if (attachment == null)
                throw new NotFoundAttachmentException(attachmentId);

            _repositoryManager.Attachment.DeleteAttachment(attachment);
            _repositoryManager.Save();
        }

        public AttachmentDTO GetAttachment(int attachmentId, bool trackChanges)
        {
            var attachment = _repositoryManager.Attachment.GetAttachment(
                attachmentId,
                trackChanges
            );
            if (attachment == null)
                throw new NotFoundAttachmentException(attachmentId);

            return _mapper.Map<AttachmentDTO>(attachment);
        }

        public IEnumerable<AttachmentDTO> GetAttachments(bool trackChanges)
        {
            var attachments = _repositoryManager.Attachment.GetAttachments(trackChanges);
            return _mapper.Map<IEnumerable<AttachmentDTO>>(attachments);
        }

        public IEnumerable<AttachmentDTO> GetAttachmentsByTaskId(int taskId, bool trackChanges)
        {
            var attachments = _repositoryManager.Attachment.GetAttachmentsByTaskId(
                taskId,
                trackChanges
            );
            return _mapper.Map<IEnumerable<AttachmentDTO>>(attachments);
        }

        public void UpdateAttachment(int attachmentId, AttachmentDTO attachment)
        {
            var attachmentDB = _repositoryManager.Attachment.GetAttachment(
                attachmentId,
                trackChanges: true
            );
            if (attachmentDB == null)
                throw new NotFoundAttachmentException(attachmentId);

            _mapper.Map(attachment, attachmentDB);
            _repositoryManager.Save();
        }
    }
}
