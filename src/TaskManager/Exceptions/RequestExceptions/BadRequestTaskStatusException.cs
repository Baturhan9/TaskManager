namespace TaskManager.Exceptions.RequestExceptions
{
    public class BadRequestTaskStatusException : BadRequestException
    {
        public BadRequestTaskStatusException(string? status)
            : base($"status '{status}' not exists") { }
    }
}
