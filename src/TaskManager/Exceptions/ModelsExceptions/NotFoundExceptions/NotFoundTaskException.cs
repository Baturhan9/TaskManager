namespace TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions
{
    public class NotFoundTaskException : NotFoundException
    {
        public NotFoundTaskException(int taskId)
            : base($"Task with id {taskId} not found.") { }
    }
}
