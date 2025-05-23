namespace TaskManager.Exceptions.RequestExceptions
{
    public class BadRequestRoleException : BadRequestException
    {
        public BadRequestRoleException(string? role)
            : base($"role {role} is not exists") { }
    }
}
