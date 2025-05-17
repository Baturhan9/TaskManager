using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDTO> GetTasks(bool trackChanges);
        TaskDTO GetTask(int taskId, bool trackChanges);
        void CreateTask(TaskDTO task);
        void UpdateTask(int taskId, TaskDTO task);
        void DeleteTask(int taskId);
        IEnumerable<TaskDTO> GetTasksByProjectId(int projectId, bool trackChanges);
        void AssignTaskToUser(int taskId, int userId, string role);

    }
}
