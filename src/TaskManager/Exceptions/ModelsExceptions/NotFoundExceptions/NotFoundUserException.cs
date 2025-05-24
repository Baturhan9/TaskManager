namespace TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions
{
    public class NotFoundUserException : NotFoundException
    {
        public NotFoundUserException(int userId)
            : base($"User with id {userId} not found.") { }
        
        /// <summary>
        /// For authentication with login and password
        /// </summary>
        public NotFoundUserException()
            : base($"Not found user with this login and password") { }
    }
}
