using Microsoft.AspNetCore.Mvc;
using TaskManager.Web.Clients;

namespace TaskManager.Web.Controllers;

public class AuthController : Controller
{
    private readonly TaskManagerClient _client;
    public AuthController(TaskManagerClient client)
    {
        _client = client;
    }
}