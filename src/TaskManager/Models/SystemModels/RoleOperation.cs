namespace TaskManager.Models.SystemModels
{
    public class RoleOperation
    {
        public Action<Models.Task, int> Assign { get; init; } = null!;
        public Func<Models.Task, int?> Retrieve { get; init; } = null!;
    }
}