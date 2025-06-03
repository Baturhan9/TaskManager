using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Web.Models.Tasks;

public class TaskDetailsViewModel
{
    public TaskDTO Task { get; set; }
    public IEnumerable<TaskStatusLogDTO> Logs { get; set; } = new List<TaskStatusLogDTO>();
    public IEnumerable<AttachmentDTO> Attachments { get; set; } = new List<AttachmentDTO>();
    public Dictionary<int, string> Usernames { get; set; } = new Dictionary<int, string>();
}