namespace TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions
{
    public class NotFoundUserAccessException : NotFoundException
    {
        public NotFoundUserAccessException(int userAccessId)
            : base($"User Access with id {userAccessId} not found") { }
    }
}
