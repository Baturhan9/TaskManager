using Microsoft.AspNetCore.Mvc;
using TaskManager.Web.Interfaces;
using TaskManager.Web.Models.AuthModels;

namespace TaskManager.Web.Controllers;

public class AuthController : Controller
{
    private readonly ITaskManagerClient _client;

    public AuthController(ITaskManagerClient client)
    {
        _client = client;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var response = await _client.RegisterAsync(request);

        if (!response.Success)
        {
            ModelState.AddModelError("", response.ErrorMessage);
            return View(request);
        }

        return RedirectToAction("Login", "Auth");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var response = await _client.LoginAsync(request);

        if (!response.Success)
        {
            ModelState.AddModelError("", response.ErrorMessage);
            return View(request);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _client.LogoutAsync();
        return RedirectToAction("Login", "Auth");
    }
}
