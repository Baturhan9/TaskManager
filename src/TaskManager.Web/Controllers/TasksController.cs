using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;
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

        var users = await _taskManagerClient.GetUsersDto();
        if (!users.Success)
        {
            return View("Error", users.ErrorMessage);
        }

        var project = await _taskManagerClient.GetProjects();
        if (!project.Success)
        {
            return View("Error", project.ErrorMessage);
        }

        var viewModel = new ListTaskPageViewModel
        {
            Tasks = listTasksViewModel,
            Users = users.Data,
            Projects = project.Data,
        };

        return View(viewModel);
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
        var usernames = await _taskManagerClient.GetUsersDto();
        if (!usernames.Success)
        {
            return View("Error", usernames.ErrorMessage);
        }

        var taskDetailsViewModel = new TaskDetailsViewModel
        {
            Task = task.Data,
            Logs = logs.Data,
            Attachments = attachments.Data,
            Usernames = usernames.Data.ToDictionary(user => user.Key, user => user.Value.Username),
        };

        return View(taskDetailsViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskForManipulationDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _taskManagerClient.CreateTaskAsync(dto);
        return RedirectToAction("List");
    }
}
