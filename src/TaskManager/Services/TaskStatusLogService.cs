using AutoMapper;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Services
{
    public class TaskStatusLogService : ITaskStatusLogService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public TaskStatusLogService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public TaskStatusLogDTO CreateTaskStatusLog(
            int taskId,
            TaskStatusLogForManipulationDTO taskStatusLog
        )
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: false);
            if (task is null)
                throw new NotFoundTaskException(taskId);
            var taskStatusLogDB = _mapper.Map<TaskStatusLog>(taskStatusLog);
            _repositoryManager.TaskStatusLog.CreateTaskStatusLog(taskStatusLogDB);
            _repositoryManager.Save();
            return _mapper.Map<TaskStatusLogDTO>(taskStatusLogDB);
        }

        public void DeleteTaskStatusLog(int taskId, int taskStatusLogId)
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: false);
            if (task is null)
                throw new NotFoundTaskException(taskId);
            var taskStatusLog = _repositoryManager.TaskStatusLog.GetTaskStatusLog(
                taskId,
                taskStatusLogId,
                trackChanges: false
            );
            if (taskStatusLog is null)
                throw new NotFoundTaskStatusLogException(taskStatusLogId);

            _repositoryManager.TaskStatusLog.DeleteTaskStatusLog(taskStatusLog);
            _repositoryManager.Save();
        }

        public TaskStatusLogDTO GetTaskStatusLog(int taskId, int taskStatusLogId, bool trackChanges)
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: false);
            if (task is null)
                throw new NotFoundTaskException(taskId);
            var taskStatusLog = _repositoryManager.TaskStatusLog.GetTaskStatusLog(
                taskId,
                taskStatusLogId,
                trackChanges
            );
            if (taskStatusLog is null)
                throw new NotFoundTaskStatusLogException(taskStatusLogId);

            return _mapper.Map<TaskStatusLogDTO>(taskStatusLog);
        }

        public IEnumerable<TaskStatusLogDTO> GetTaskStatusLogs(int taskId, bool trackChanges)
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: false);
            if (task is null)
                throw new NotFoundTaskException(taskId);
            var taskStatusLogs = _repositoryManager.TaskStatusLog.GetTaskStatusLogs(
                taskId,
                trackChanges
            );
            return _mapper.Map<IEnumerable<TaskStatusLogDTO>>(taskStatusLogs);
        }

        public void UpdateTaskStatusLog(
            int taskId,
            int taskStatusLogId,
            TaskStatusLogForManipulationDTO taskStatusLog
        )
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: false);
            var taskStatusLogDB = _repositoryManager.TaskStatusLog.GetTaskStatusLog(
                taskId,
                taskStatusLogId,
                trackChanges: true
            );
            if (taskStatusLogDB is null)
                throw new NotFoundTaskStatusLogException(taskStatusLogId);

            _mapper.Map(taskStatusLog, taskStatusLogDB);
            _repositoryManager.Save();
        }
    }
}
