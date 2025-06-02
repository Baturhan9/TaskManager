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
        var user = await _client.GetCurrentUserAsync();
        if (user == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        return View(user);
    }
}