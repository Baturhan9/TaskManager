using Microsoft.AspNetCore.Mvc;
using TaskManager.Web.Interfaces;

namespace TaskManager.Web.Controllers;

public class UserController : Controller
{
    private readonly ITaskManagerClient _client;

    public UserController(ITaskManagerClient client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var response = await _client.GetCurrentUserAsync();
        if (!response.Success)
        {
            ModelState.AddModelError("", response.ErrorMessage);
            return View();
        }
        return View(response.Data);
    }
}
