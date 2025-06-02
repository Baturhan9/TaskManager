using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
}