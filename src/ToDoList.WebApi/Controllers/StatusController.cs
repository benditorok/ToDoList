using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ToDoList.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private ILogger<StatusController> _logger;

    public StatusController(ILogger<StatusController> logger)
    {
        _logger = logger; 
    }

    // TODO make this better, limit rate
    [HttpGet, HttpPost]
    public Task<IActionResult> Get()
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var ip = HttpContext.Connection.RemoteIpAddress;
        _logger?.LogInformation("Ping from {id}, user {user}", ip?.ToString(), user?.Value);

        return Task.FromResult<IActionResult>(Ok());
    }
}
