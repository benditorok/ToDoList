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
        var ip = HttpContext.Connection.RemoteIpAddress;
        _logger?.LogInformation("Status pinged from {id}", ip?.ToString());

        return Task.FromResult<IActionResult>(Ok());
    }
}
