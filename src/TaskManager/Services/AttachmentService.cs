using AutoMapper;
using TaskManager.Exceptions.ForbiddenExceptions;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

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

        public AttachmentDTO CreateAttachment(
            int taskId,
            int userId,
            AttachmentForManipulationDTO attachment
        )
        {
            CheckPermission(taskId, userId);

            var attachmentDB = _mapper.Map<Attachment>(attachment);
            _repositoryManager.Attachment.CreateAttachment(attachmentDB);
            _repositoryManager.Save();
            return _mapper.Map<AttachmentDTO>(attachmentDB);
        }

        public void DeleteAttachment(int taskId, int attachmentId, int userId)
        {
            CheckPermission(taskId, userId);
            var attachment = _repositoryManager.Attachment.GetAttachment(
                taskId,
                attachmentId,
                trackChanges: true
            );
            if (attachment is null)
                throw new NotFoundAttachmentException(attachmentId);

            _repositoryManager.Attachment.DeleteAttachment(attachment);
            _repositoryManager.Save();
        }

        public AttachmentDTO GetAttachment(int taskId, int attachmentId, int userId, bool trackChanges)
        {
            CheckPermission(taskId, userId);
            var attachment = _repositoryManager.Attachment.GetAttachment(
                taskId,
                attachmentId,
                trackChanges
            );
            if (attachment is null)
                throw new NotFoundAttachmentException(attachmentId);
            return _mapper.Map<AttachmentDTO>(attachment);
        }

        public IEnumerable<AttachmentDTO> GetAttachments(int taskId, int userId, bool trackChanges)
        {
            CheckPermission(taskId, userId);
            var attachments = _repositoryManager.Attachment.GetAttachments(taskId, trackChanges);
            return _mapper.Map<IEnumerable<AttachmentDTO>>(attachments);
        }

        public void UpdateAttachment(
            int taskId,
            int attachmentId,
            int userId,
            AttachmentForManipulationDTO attachment
        )
        {
            CheckPermission(taskId, userId);
            var attachmentDB = _repositoryManager.Attachment.GetAttachment(
                taskId,
                attachmentId,
                trackChanges: true
            );
            if (attachmentDB is null)
                throw new NotFoundAttachmentException(attachmentId);

            _mapper.Map(attachment, attachmentDB);
            _repositoryManager.Save();
        }

        private void CheckPermission(int taskId, int userId)
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: false);
            if (task is null)
                throw new NotFoundTaskException(taskId);

            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);

            var access = _repositoryManager.UserAccess.GetUserAccessesByUserIdAndProjectId(
                userId,
                task.ProjectId.Value,
                trackChanges: false
            );
            if (access is null)
                throw new ProjectForbiddenException(task.ProjectId.Value);
        }
    }
}
