namespace TaskManager.Exceptions.RequestExceptions
{
    public class BadRequestSameStatusTaskException : BadRequestException
    {
        public BadRequestSameStatusTaskException(int taskId, string status)
            : base($"task with id {taskId} already in status {status}") { }
    }
}
