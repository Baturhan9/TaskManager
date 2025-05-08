// interface for service manager where all services

namespace TaskManager.Interfaces.Services
{
    public interface IServiceManager
    {
        IAttachmentService Attachment { get; }
        IProjectService Project { get; }
        ITaskService Task { get; }
        ITaskStatusLogService TaskStatusLog { get; }
        IUserAccessService UserAccess { get; }
        IUserService User { get; }
    }
}
