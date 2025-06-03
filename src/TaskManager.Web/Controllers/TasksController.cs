using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Web.Interfaces;
using TaskManager.Web.Models.Tasks;

namespace TaskManager.Web.Controllers;

public class TasksController : Controller
{
    private readonly ITaskManagerClient _taskManagerClient;
    private readonly IMapper _mapper;

    public TasksController(ITaskManagerClient taskManagerClient, IMapper mapper)
    {
        _taskManagerClient = taskManagerClient;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var response = await _taskManagerClient.GetAllTasks();
        if (!response.Success)
        {
            return View("Error", response.ErrorMessage);
        }
        var listTasksViewModel = _mapper.Map<IEnumerable<ListTasksViewModel>>(response.Data);

        return View(listTasksViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var task = await _taskManagerClient.GetTask(id);
        if (!task.Success)
        {
            return View("Error", task.ErrorMessage);
        }

        var logs = await _taskManagerClient.GetTaskLogs(id);
        if (!logs.Success)
        {
            logs.Data = new List<TaskStatusLogDTO>();
        }

        var attachments = await _taskManagerClient.GetTaskAttachments(id);
        if (!attachments.Success)
        {
            attachments.Data = new List<AttachmentDTO>();
        }
        var ids = await GetAllIdsInTaskDetails(task.Data, logs.Data);
        var usernames = await _taskManagerClient.GetUsernamesByIds(ids);
        if (!usernames.Success)
        {
            return View("Error", usernames.ErrorMessage);
        }

        var taskDetailsViewModel = new TaskDetailsViewModel
        {
            Task = task.Data,
            Logs = logs.Data,
            Attachments = attachments.Data,
            Usernames = usernames.Data,
        };

        return View(taskDetailsViewModel);
    }


    private async Task<List<int>> GetAllIdsInTaskDetails(TaskDTO task, IEnumerable<TaskStatusLogDTO> logs)
    {
        var ids = new List<int>
        {
            task.AssignmentId ?? 0,
            task.AuthorId ?? 0,
            task.ReviewerId ?? 0,
            task.TesterId ?? 0
        };
        ids.AddRange(logs.Select(log => log.UserId ?? 0));
        return ids.Distinct().ToList();
    }
}