using AutoMapper;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Services
{
    public class AttachmentService : IAttachmentService
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

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
            var attachment = _repositoryManager.Attachment.GetAttachment(attachmentId, trackChanges: false);
            if (attachment == null)
                throw new Exception($"Attachment with id {attachmentId} not found.");

            _repositoryManager.Attachment.DeleteAttachment(attachment);
            _repositoryManager.Save();
        }

        public AttachmentDTO GetAttachment(int attachmentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttachmentDTO> GetAttachments(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void UpdateAttachment(int attachmentId, AttachmentDTO attachment)
        {
            throw new NotImplementedException();
        }
    }
}