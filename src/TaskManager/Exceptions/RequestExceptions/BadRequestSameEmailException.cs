namespace TaskManager.Exceptions.RequestExceptions
{
    public class BadRequestSameEmailException : BadRequestException
    {
        public BadRequestSameEmailException(string email)
            : base($"a user with email {email} is already registered, use a different one.") { }
    }
}
