using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<Models.Task> GetTasks(bool trackChanges);
        IEnumerable<Models.Task> GetTasksByProjectId(int projectId, bool trackChanges);
        Models.Task GetTask(int taskId, bool trackChanges);
        void DeleteTask(Models.Task task);
        void CreateTask(Models.Task task);
    }
}
