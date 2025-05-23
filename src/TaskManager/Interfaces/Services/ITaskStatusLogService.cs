using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface ITaskStatusLogService
    {
        IEnumerable<TaskStatusLogDTO> GetTaskStatusLogs(bool trackChanges);
        IEnumerable<TaskStatusLogDTO> GetTaskStatusLogsByTaskId(int taskId, bool trackChanges);
        TaskStatusLogDTO GetTaskStatusLog(int taskStatusLogId, bool trackChanges);
        TaskStatusLogDTO CreateTaskStatusLog(TaskStatusLogForManipulationDTO taskStatusLog);
        void UpdateTaskStatusLog(
            int taskStatusLogId,
            TaskStatusLogForManipulationDTO taskStatusLog
        );
        void DeleteTaskStatusLog(int taskStatusLogId);
    }
}
