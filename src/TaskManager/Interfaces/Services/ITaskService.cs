using TaskManager.Consts;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDTO> GetTasks(int userId, bool trackChanges);
        IEnumerable<TaskDTO> GetTasksByUserRole(int userId, TaskRoles userRole, bool trackChanges);
        IEnumerable<TaskDTO> GetTasksByProjectId(int projectId, int userId, bool trackChanges);
        TaskDTO GetTask(int taskId, int userId, bool trackChanges);
        TaskDTO CreateTask(int userId, TaskForManipulationDTO task);
        void UpdateTask(int taskId, int userId, TaskForManipulationDTO task);
        void DeleteTask(int taskId, int userId);
        void AssignTaskToUser(int taskId, int userId, int toUserId, TaskRoles role);
        void ChangeTaskStatus(int taskId, int userId, TaskStatuses status);
    }
}
