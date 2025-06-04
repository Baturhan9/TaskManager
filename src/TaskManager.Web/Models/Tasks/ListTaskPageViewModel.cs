using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.Shared;

namespace TaskManager.Web.Models.Tasks;

public class ListTaskPageViewModel
{
    public IEnumerable<ListTasksViewModel> Tasks { get; set; } = new List<ListTasksViewModel>();
    public Dictionary<string, UserListDto> Users { get; set; } = new();
    public IEnumerable<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
}