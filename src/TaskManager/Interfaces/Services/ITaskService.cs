using TaskManager.Consts;
using TaskManager.Models.CreateModelObjects;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDTO> GetTasks(bool trackChanges);
        IEnumerable<TaskDTO> GetTasksByUserRole(int userId, TaskRoles userRole, bool trackChanges);
        IEnumerable<TaskDTO> GetTasksByProjectId(int projectId, bool trackChanges);
        TaskDTO GetTask(int taskId, bool trackChanges);
        void CreateTask(TaskCreateDTO task);
        void UpdateTask(int taskId, TaskCreateDTO task);
        void DeleteTask(int taskId);
        void AssignTaskToUser(int taskId, int userId, TaskRoles role);
    }
}
