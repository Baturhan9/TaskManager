using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface ITaskStatusLogService
    {
        IEnumerable<TaskStatusLogDTO> GetTaskStatusLogs(int taskId, int userId, bool trackChanges);
        TaskStatusLogDTO GetLastStatusLog(int taskId, int userId, bool trackChanges);
        TaskStatusLogDTO GetTaskStatusLog(
            int taskId,
            int taskStatusLogId,
            int userId,
            bool trackChanges
        );
        TaskStatusLogDTO CreateTaskStatusLog(
            int taskId,
            int userId,
            TaskStatusLogForManipulationDTO taskStatusLog
        );
        void UpdateTaskStatusLog(
            int taskId,
            int taskStatusLogId,
            int userId,
            TaskStatusLogForManipulationDTO taskStatusLog
        );
        void DeleteTaskStatusLog(int taskId, int taskStatusLogId, int userId);
    }
}
