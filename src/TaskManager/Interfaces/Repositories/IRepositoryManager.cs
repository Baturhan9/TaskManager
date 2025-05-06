namespace TaskManager.Interfaces.Repositories
{
    public interface IRepositoryManager
    {
        IAttachmentRepository Attachment { get; }
        ITaskStatusLogRepository TaskStatusLog { get; }
        ITaskRepository Task { get; }
        IUserRepository User { get; }
        IProjectRepository Project { get; }
        IUserAccessRepository UserAccess { get; }
        void Save();
    }
}
