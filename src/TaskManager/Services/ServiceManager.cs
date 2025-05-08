using AutoMapper;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;

namespace TaskManager.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAttachmentService> _attachmentService;
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<ITaskService> _taskService;
        private readonly Lazy<ITaskStatusLogService> _taskStatusLogService;
        private readonly Lazy<IUserAccessService> _userAccessService;
        private readonly Lazy<IUserService> _userService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _attachmentService = new Lazy<IAttachmentService>(() =>
                new AttachmentService(repositoryManager, mapper)
            );
            _projectService = new Lazy<IProjectService>(() =>
                new ProjectService(repositoryManager, mapper)
            );
            _taskService = new Lazy<ITaskService>(() => new TaskService(repositoryManager, mapper));
            _taskStatusLogService = new Lazy<ITaskStatusLogService>(() =>
                new TaskStatusLogService(repositoryManager, mapper)
            );
            _userAccessService = new Lazy<IUserAccessService>(() =>
                new UserAccessService(repositoryManager, mapper)
            );
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
        }

        public IAttachmentService Attachment => _attachmentService.Value;
        public IProjectService Project => _projectService.Value;
        public ITaskService Task => _taskService.Value;
        public ITaskStatusLogService TaskStatusLog => _taskStatusLogService.Value;
        public IUserAccessService UserAccess => _userAccessService.Value;
        public IUserService User => _userService.Value;
    }
}
