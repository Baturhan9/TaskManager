using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    interface ITaskStatusLogRepository
    {
        IEnumerable<TaskStatusLog> GetTaskStatusLogs(bool trackChanges);
        IEnumerable<TaskStatusLog> GetTaskStatusLogsByTaskId(int taskStatusLogId, bool trackChanges);
        TaskStatusLog GetTaskStatusLog(int taskId, bool trackChanges);
        void DeleteTaskStatusLog(TaskStatusLog task);
        void CreateTaskStatusLog(TaskStatusLog task);
    }
}