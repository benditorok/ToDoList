using Microsoft.AspNetCore.Mvc;

namespace ToDoList.WebApi.Controllers;
[Route("[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    [HttpGet, HttpPost]
    public IActionResult Get()
    {
        return Ok();
    }
}
