using TaskManager.Consts;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDTO> GetTasks(int userId, bool trackChanges);
        IEnumerable<TaskDTO> GetTasksByUserRole(int userId, TaskRoles userRole, bool trackChanges);
        IEnumerable<TaskDTO> GetTasksByProjectId(int projectId, bool trackChanges);
        TaskDTO GetTask(int taskId, bool trackChanges);
        TaskDTO CreateTask(TaskForManipulationDTO task);
        void UpdateTask(int taskId, TaskForManipulationDTO task);
        void DeleteTask(int taskId);
        void AssignTaskToUser(int taskId, int userId, TaskRoles role);
    }
}
