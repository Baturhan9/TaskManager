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

        public void CreateTaskStatusLog(TaskStatusLogForManipulationDTO taskStatusLog)
        {
            var taskStatusLogDB = _mapper.Map<TaskStatusLog>(taskStatusLog);
            _repositoryManager.TaskStatusLog.CreateTaskStatusLog(taskStatusLogDB);
            _repositoryManager.Save();
        }

        public void DeleteTaskStatusLog(int taskStatusLogId)
        {
            var taskStatusLog = _repositoryManager.TaskStatusLog.GetTaskStatusLog(
                taskStatusLogId,
                trackChanges: false
            );
            if (taskStatusLog == null)
                throw new NotFoundTaskStatusLogException(taskStatusLogId);

            _repositoryManager.TaskStatusLog.DeleteTaskStatusLog(taskStatusLog);
            _repositoryManager.Save();
        }

        public TaskStatusLogDTO GetTaskStatusLog(int taskStatusLogId, bool trackChanges)
        {
            var taskStatusLog = _repositoryManager.TaskStatusLog.GetTaskStatusLog(
                taskStatusLogId,
                trackChanges
            );
            if (taskStatusLog == null)
                throw new NotFoundTaskStatusLogException(taskStatusLogId);

            return _mapper.Map<TaskStatusLogDTO>(taskStatusLog);
        }

        public IEnumerable<TaskStatusLogDTO> GetTaskStatusLogs(bool trackChanges)
        {
            var taskStatusLogs = _repositoryManager.TaskStatusLog.GetTaskStatusLogs(trackChanges);
            return _mapper.Map<IEnumerable<TaskStatusLogDTO>>(taskStatusLogs);
        }

        public IEnumerable<TaskStatusLogDTO> GetTaskStatusLogsByTaskId(
            int taskId,
            bool trackChanges
        )
        {
            var task = _repositoryManager.Task.GetTask(taskId, trackChanges: false);
            if (task is null)
                throw new NotFoundTaskException(taskId);

            var taskStatusLog = _repositoryManager.TaskStatusLog.GetTaskStatusLogsByTaskId(
                taskId,
                trackChanges
            );
            return _mapper.Map<IEnumerable<TaskStatusLogDTO>>(taskStatusLog);
        }

        public void UpdateTaskStatusLog(
            int taskStatusLogId,
            TaskStatusLogForManipulationDTO taskStatusLog
        )
        {
            var taskStatusLogDB = _repositoryManager.TaskStatusLog.GetTaskStatusLog(
                taskStatusLogId,
                trackChanges: true
            );
            if (taskStatusLogDB == null)
                throw new NotFoundTaskStatusLogException(taskStatusLogId);

            _mapper.Map(taskStatusLog, taskStatusLogDB);
            _repositoryManager.Save();
        }
    }
}
