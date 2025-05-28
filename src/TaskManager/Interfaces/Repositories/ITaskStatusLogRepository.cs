using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface ITaskStatusLogRepository
    {
        IEnumerable<TaskStatusLog> GetTaskStatusLogs(int taskId, bool trackChanges);
        TaskStatusLog GetTaskStatusLog(int taskId, int taskStatusLogId, bool trackChanges);
        TaskStatusLog GetLastTaskStatusLog(int taskId, bool trackChanges);
        void DeleteTaskStatusLog(TaskStatusLog taskStatusLog);
        void CreateTaskStatusLog(TaskStatusLog taskStatusLog);
    }
}
