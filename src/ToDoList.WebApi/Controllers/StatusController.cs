using Microsoft.AspNetCore.Mvc;

namespace ToDoList.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    [HttpGet, HttpPost]
    public Task<IActionResult> Get()
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}
