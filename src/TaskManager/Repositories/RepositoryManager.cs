using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly TaskManagerContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IAttachmentRepository> _attachmentRepository;
        private readonly Lazy<IUserAccessRepository> _userAccessRepository;
        private readonly Lazy<ITaskStatusLogRepository> _taskStatusLogRepository;
        private readonly Lazy<ITaskRepository> _taskRepository;
        private readonly Lazy<IProjectRepository> _projectRepository;

        public RepositoryManager(TaskManagerContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _attachmentRepository = new Lazy<IAttachmentRepository>(() =>
                new AttachmentRepository(_context)
            );
            _userAccessRepository = new Lazy<IUserAccessRepository>(() =>
                new UserAccessRepository(_context)
            );
            _taskStatusLogRepository = new Lazy<ITaskStatusLogRepository>(() =>
                new TaskStatusLogRepository(_context)
            );
            _taskRepository = new Lazy<ITaskRepository>(() => new TaskRepository(_context));
            _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(_context)
            );
        }

        public IUserRepository User => _userRepository.Value;
        public IAttachmentRepository Attachment => _attachmentRepository.Value;
        public IUserAccessRepository UserAccess => _userAccessRepository.Value;
        public ITaskStatusLogRepository TaskStatusLog => _taskStatusLogRepository.Value;
        public ITaskRepository Task => _taskRepository.Value;
        public IProjectRepository Project => _projectRepository.Value;

        public void Save() => _context.SaveChanges();
    }
}
