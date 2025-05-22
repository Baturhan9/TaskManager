using AutoMapper;
using TaskManager.Consts;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Exceptions.RequestExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.SystemModels;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        private readonly Dictionary<TaskRoles, RoleOperation> _roleOperation = new()
        {
            {
                TaskRoles.Developer,
                new RoleOperation
                {
                    Assign = (task, userId) => task.AssignmentId = userId,
                    Retrieve = task => task.AssignmentId,
                }
            },
            {
                TaskRoles.Tester,
                new RoleOperation
                {
                    Assign = (task, userId) => task.TesterId = userId,
                    Retrieve = task => task.TesterId,
                }
            },
            {
                TaskRoles.Reviewer,
                new RoleOperation
                {
                    Assign = (task, userId) => task.ReviewerId = userId,
                    Retrieve = task => task.ReviewerId,
                }
            },
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

            if (!_roleOperation.TryGetValue(role, out var assignRole))
                throw new BadRequestRoleException(role.ToString());

            assignRole.Assign(task, userId);

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
            var project = _repositoryManager.Project.GetProject(projectId, trackChanges: false);

            if (project is null)
                throw new NotFoundProjectException(projectId);

            var tasks = _repositoryManager.Task.GetTasksByProjectId(projectId, trackChanges);
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public IEnumerable<TaskDTO> GetTasksByUserRole(
            int userId,
            TaskRoles userRole,
            bool trackChanges
        )
        {
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);

            if (!_roleOperation.TryGetValue(userRole, out var operation))
                throw new BadRequestRoleException(userRole.ToString());

            var tasks = _repositoryManager.Task.GetTasks(trackChanges);
            tasks = tasks.Where(t => operation.Retrieve(t) == userId);

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
