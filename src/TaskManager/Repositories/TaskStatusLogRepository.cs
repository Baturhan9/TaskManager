using TaskManager.Consts;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class TaskStatusLogRepository : RepositoryBase<TaskStatusLog>, ITaskStatusLogRepository
    {
        public TaskStatusLogRepository(TaskManagerContext context)
            : base(context) { }

        public IEnumerable<TaskStatusLog> GetTaskStatusLogs(int taskId, bool trackChanges) =>
            FindByCondition(l => l.TaskId.Equals(taskId), trackChanges)
                .OrderByDescending(t => t.DateUpdate)
                .ToList();

        public TaskStatusLog GetTaskStatusLog(int taskId, int taskStatusLogId, bool trackChanges) =>
            FindByCondition(
                    t => t.TaskStatusId.Equals(taskStatusLogId) && t.TaskId.Equals(taskId),
                    trackChanges
                )
                .SingleOrDefault();

        public void DeleteTaskStatusLog(TaskStatusLog taskStatusLog) => Delete(taskStatusLog);

        public void CreateTaskStatusLog(TaskStatusLog taskStatusLog) => Create(taskStatusLog);

        public TaskStatusLog GetLastTaskStatusLog(int taskId, bool trackChanges) =>
            FindByCondition(
                    t => t.TaskId.Equals(taskId) && t.Status != TaskStatuses.Empty.ToString(),
                    trackChanges
                )
                .OrderByDescending(t => t.DateUpdate)
                .FirstOrDefault();
    }
}
