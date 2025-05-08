namespace TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions
{
    public class NotFoundTaskStatusLogException : NotFoundException
    {
        public NotFoundTaskStatusLogException(int taskStatusLogId)
            : base($"Task status log with id {taskStatusLogId} not found.") { }
    }
}
