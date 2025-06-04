using TaskManager.Consts;

namespace TaskManager.Web.Models.Tasks;
public class ListTasksViewModel
{
    public int TaskId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime Deadline { get; init; }
    public string Status { get; init; } = TaskStatuses.UnKnown.ToString();
}