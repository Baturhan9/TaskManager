using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface ITaskStatusLogService
    {
        IEnumerable<TaskStatusLogDTO> GetTaskStatusLogs(int taskId, bool trackChanges);
        TaskStatusLogDTO GetTaskStatusLog(int taskId, int taskStatusLogId, bool trackChanges);
        TaskStatusLogDTO CreateTaskStatusLog(int taskId, TaskStatusLogForManipulationDTO taskStatusLog);
        void UpdateTaskStatusLog(
            int taskId,
            int taskStatusLogId,
            TaskStatusLogForManipulationDTO taskStatusLog
        );
        void DeleteTaskStatusLog(int taskId, int taskStatusLogId);
    }
}
