using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface ITaskStatusLogService
    {
        IEnumerable<TaskStatusLogDTO> GetTaskStatusLogs(bool trackChanges);
        IEnumerable<TaskStatusLogDTO> GetTaskStatusLogsByTaskId(int taskId, bool trackChanges);
        TaskStatusLogDTO GetTaskStatusLog(int taskStatusLogId, bool trackChanges);
        void CreateTaskStatusLog(TaskStatusLogDTO taskStatusLog);
        void UpdateTaskStatusLog(int taskStatusLogId, TaskStatusLogDTO taskStatusLog);
        void DeleteTaskStatusLog(int taskStatusLogId);
    }
}
