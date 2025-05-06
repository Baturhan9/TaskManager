using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class TaskStatusLogRepository : RepositoryBase<TaskStatusLog>, ITaskStatusLogRepository
    {
        public TaskStatusLogRepository(TaskManagerContext context)
            : base(context) { }

        public IEnumerable<TaskStatusLog> GetTaskStatusLogs(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(t => t.TaskId).ToList();

        public IEnumerable<TaskStatusLog> GetTaskStatusLogsByTaskId(
            int taskId,
            bool trackChanges
        ) => FindByCondition(t => t.TaskId.Equals(taskId), trackChanges).ToList();

        public TaskStatusLog GetTaskStatusLog(int taskStatusLogId, bool trackChanges) =>
            FindByCondition(t => t.TaskStatusId.Equals(taskStatusLogId), trackChanges)
                .SingleOrDefault();

        public void DeleteTaskStatusLog(TaskStatusLog taskStatusLog) => Delete(taskStatusLog);

        public void CreateTaskStatusLog(TaskStatusLog taskStatusLog) => Create(taskStatusLog);
    }
}
