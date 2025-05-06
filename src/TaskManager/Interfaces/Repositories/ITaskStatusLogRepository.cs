using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface ITaskStatusLogRepository
    {
        IEnumerable<TaskStatusLog> GetTaskStatusLogs(bool trackChanges);
        IEnumerable<TaskStatusLog> GetTaskStatusLogsByTaskId(
            int taskStatusLogId,
            bool trackChanges
        );
        TaskStatusLog GetTaskStatusLog(int taskStatusLogId, bool trackChanges);
        void DeleteTaskStatusLog(TaskStatusLog taskStatusLog);
        void CreateTaskStatusLog(TaskStatusLog taskStatusLog);
    }
}
