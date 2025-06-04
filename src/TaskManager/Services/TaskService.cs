using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManager.Consts;
using TaskManager.Exceptions.ForbiddenExceptions;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Exceptions.RequestExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;
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

        public void AssignTaskToUser(int taskId, int userId, int toUserId, TaskRoles role)
        {
            var task = TryGetTask(taskId, userId, trackChanges: true);

            if (!_roleOperation.TryGetValue(role, out var assignRole))
                throw new BadRequestRoleException(role.ToString());

            assignRole.Assign(task, toUserId);

            _repositoryManager.Save();
        }

        public void ChangeTaskStatus(int taskId, int userId, TaskStatuses status)
        {
            var task = TryGetTask(taskId, userId, trackChanges: false);
            var lastStatus = _repositoryManager.TaskStatusLog.GetLastTaskStatusLog(
                taskId,
                trackChanges: false
            );
            if (lastStatus.Status == status.ToString())
                throw new BadRequestSameStatusTaskException(taskId, status.ToString());

            var log = new TaskStatusLogForManipulationDTO()
            {
                TaskId = task.TaskId,
                UserId = userId,
                Status = status.ToString(),
                DateUpdate = DateTime.UtcNow,
            };

            var taskStatusLogDB = _mapper.Map<TaskStatusLog>(log);
            _repositoryManager.TaskStatusLog.CreateTaskStatusLog(taskStatusLogDB);
            _repositoryManager.Save();
        }

        public TaskDTO CreateTask(int userId, TaskForManipulationDTO task)
        {
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);

            var access = _repositoryManager.UserAccess.GetUserAccessesByUserIdAndProjectId(
                userId,
                task.ProjectId.Value,
                trackChanges: false
            );
            if (access is null)
                throw new ProjectForbiddenException(task.ProjectId.Value);

            var taskDB = _mapper.Map<Models.Task>(task);
            _repositoryManager.Task.CreateTask(taskDB);
            _repositoryManager.Save();

            var log = new TaskStatusLog()
            {
                TaskId = taskDB.TaskId,
                UserId = userId,
                Status = TaskStatuses.New.ToString(),
                Comment = "Task is created",
                DateUpdate = DateTime.UtcNow,
            };

            _repositoryManager.TaskStatusLog.CreateTaskStatusLog(log);
            _repositoryManager.Save();

            var taskDto = _mapper.Map<TaskDTO>(taskDB);
            taskDto.LastStatus = TaskStatuses.New.ToString();
            return _mapper.Map<TaskDTO>(taskDB);
        }

        public void DeleteTask(int taskId, int userId)
        {
            var task = TryGetTask(taskId, userId, trackChanges: false);
            _repositoryManager.Task.DeleteTask(task);
            _repositoryManager.Save();
        }

        public TaskDTO GetTask(int taskId, int userId, bool trackChanges)
        {
            var task = TryGetTask(taskId, userId, trackChanges);
            return GetTaskWithLastStatus(_mapper.Map<TaskDTO>(task));
        }

        public IEnumerable<TaskDTO> GetTasks(int userId, bool trackChanges)
        {
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);

            var accessesProjectIds = _repositoryManager
                .UserAccess.GetUserAccessesByUserId(userId, trackChanges: false)
                .Select(a => a.ProjectId);

            var tasks = _repositoryManager
                .Task.GetTasks(trackChanges)
                .Where(t => accessesProjectIds.Contains(t.ProjectId));
            return GetTasksWithLastStatus(_mapper.Map<IEnumerable<TaskDTO>>(tasks));
        }

        public IEnumerable<TaskDTO> GetTasksByProjectId(
            int projectId,
            int userId,
            bool trackChanges
        )
        {
            var project = _repositoryManager.Project.GetProject(projectId, trackChanges: false);
            if (project is null)
                throw new NotFoundProjectException(projectId);

            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);

            var access = _repositoryManager.UserAccess.GetUserAccessesByUserIdAndProjectId(
                userId,
                projectId,
                trackChanges: false
            );
            if (access is null)
                throw new ProjectForbiddenException(projectId);

            var tasks = _repositoryManager.Task.GetTasksByProjectId(projectId, trackChanges);
            return GetTasksWithLastStatus(_mapper.Map<IEnumerable<TaskDTO>>(tasks));
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

            var accessesProjectIds = _repositoryManager
                .UserAccess.GetUserAccessesByUserId(userId, trackChanges: false)
                .Select(a => a.ProjectId);

            var tasks = _repositoryManager
                .Task.GetTasks(trackChanges)
                .Where(t => accessesProjectIds.Contains(t.ProjectId));

            tasks = tasks.Where(t => operation.Retrieve(t) == userId);

            return GetTasksWithLastStatus(_mapper.Map<IEnumerable<TaskDTO>>(tasks));
        }

        public void UpdateTask(int taskId, int userId, TaskForManipulationDTO task)
        {
            var taskDB = TryGetTask(taskId, userId, trackChanges: true);
            _mapper.Map(task, taskDB);
            _repositoryManager.Save();
        }

        private Models.Task TryGetTask(int taskId, int userId, bool trackChanges)
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges);
            if (task is null)
                throw new NotFoundTaskException(taskId);
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);
            var access = _repositoryManager.UserAccess.GetUserAccessesByUserIdAndProjectId(
                userId,
                task.ProjectId.Value,
                trackChanges: false
            );
            if (access is null)
                throw new ProjectForbiddenException(task.ProjectId.Value);
            return task;
        }

        private IEnumerable<TaskDTO> GetTasksWithLastStatus(IEnumerable<TaskDTO> tasks)
        {
            foreach (var taskDto in tasks)
            {
                yield return GetTaskWithLastStatus(taskDto);
            }
        }

        private TaskDTO GetTaskWithLastStatus(TaskDTO taskDto)
        {
            var log = _repositoryManager.TaskStatusLog.GetLastTaskStatusLog(
                taskDto.TaskId,
                trackChanges: false
            );

            string? lastStatus = log?.Status;
            if (lastStatus is not null)
            {
                taskDto.LastStatus = lastStatus;
            }

            return taskDto;
        }
    }
}
