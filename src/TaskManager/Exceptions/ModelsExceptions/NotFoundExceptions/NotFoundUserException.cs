namespace TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions
{
    public class NotFoundUserException : NotFoundException
    {
        public NotFoundUserException(int userId)
            : base($"User with id {userId} not found.") { }
    }
}
