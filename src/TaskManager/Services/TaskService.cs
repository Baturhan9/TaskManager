using AutoMapper;
using TaskManager.Consts;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        private readonly Dictionary<TaskRoles, Action<Models.Task, int>> _roleAssignments = new()
        {
            { TaskRoles.Developer, (task, userId) => task.AssignmentId = userId },
            { TaskRoles.Tester, (task, userId) => task.TesterId = userId },
            { TaskRoles.Reviewer, (task, userId) => task.ReviewerId = userId },
        };

        public TaskService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void AssignTaskToUser(int taskId, int userId, TaskRoles role)
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: true);
            if (task == null)
                throw new NotFoundTaskException(taskId);
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user == null)
                throw new NotFoundUserException(userId);

            if (!_roleAssignments.TryGetValue(role, out var assignRole))
                throw new InvalidOperationException($"Invalid role assignment: {role}"); // TODO: Create Custom Exception

            assignRole(task, userId);

            _repositoryManager.Save();
        }

        public void CreateTask(TaskDTO task)
        {
            var taskDB = _mapper.Map<Models.Task>(task);
            _repositoryManager.Task.CreateTask(taskDB);
            _repositoryManager.Save();
        }

        public void DeleteTask(int taskId)
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: false);
            if (task == null)
                throw new NotFoundTaskException(taskId);

            _repositoryManager.Task.DeleteTask(task);
            _repositoryManager.Save();
        }

        public TaskDTO GetTask(int taskId, bool trackChanges)
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges);
            if (task == null)
                throw new NotFoundTaskException(taskId);

            return _mapper.Map<TaskDTO>(task);
        }

        public IEnumerable<TaskDTO> GetTasks(bool trackChanges)
        {
            var tasks = _repositoryManager.Task.GetTasks(trackChanges);
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public IEnumerable<TaskDTO> GetTasksByProjectId(int projectId, bool trackChanges)
        {
            var tasks = _repositoryManager.Task.GetTasksByProjectId(projectId, trackChanges);
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public void UpdateTask(int taskId, TaskDTO task)
        {
            var taskDB = _repositoryManager.Task.GetTask(taskId, trackChanges: true);
            if (taskDB == null)
                throw new NotFoundTaskException(taskId);

            _mapper.Map(task, taskDB);
            _repositoryManager.Save();
        }
    }
}
