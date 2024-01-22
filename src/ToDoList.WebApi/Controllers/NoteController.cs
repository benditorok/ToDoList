using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Notes;
using ToDoList.Domain.Entities;

namespace ToDoList.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class NoteController : ControllerBase
{
    private NoteLogic _logic;

    public NoteController(NoteLogic logic)
    {
        _logic = logic;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<IActionResult> CreateAsync([FromBody] Note value)
    {
        try
        {
            await _logic.CreateAsync(value);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<IActionResult> ReadAsync(int id)
    {
        try
        {
            return Ok(await _logic.ReadAsync(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<IActionResult> UpdateAsync([FromBody] Note value)
    {
        try
        {
            await _logic.UpdateAsync(value);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _logic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
