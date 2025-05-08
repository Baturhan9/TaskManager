namespace TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions
{
    public class NotFoundProjectException : NotFoundException
    {
        public NotFoundProjectException(int projectId)
            : base($"Project with id {projectId} not found.") { }
    }
}
