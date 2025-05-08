namespace TaskManager.Models.DataTransferObjects
{
    public class UserAccessDTO
    {
        public int UserAccessId { get; init; }
        public int? UserId { get; init; }
        public int? ProjectId { get; init; }
    }
}
