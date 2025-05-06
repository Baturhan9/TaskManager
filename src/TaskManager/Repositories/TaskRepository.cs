using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class TaskRepository : RepositoryBase<Models.Task>, ITaskRepository
    {
        public TaskRepository(TaskManagerContext context)
            : base(context) { }

        public IEnumerable<Models.Task> GetTasks(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(t => t.TaskId).ToList();

        public Models.Task GetTask(int taskId, bool trackChanges) =>
            FindByCondition(t => t.TaskId.Equals(taskId), trackChanges).SingleOrDefault();

        public void CreateTask(Models.Task task) => Create(task);

        public void DeleteTask(Models.Task task) => Delete(task);

        public IEnumerable<Models.Task> GetTasksByProjectId(int projectId, bool trackChanges) =>
            FindByCondition(t => t.ProjectId.Equals(projectId), trackChanges).ToList();
    }
}
