namespace TaskManager.Exceptions.ForbiddenExceptions
{
    public class ProjectForbiddenException : ForbiddenException
    {
        public ProjectForbiddenException(int projectId)
            : base($"You do not have access to the project with this id {projectId}") { }
    }
}
