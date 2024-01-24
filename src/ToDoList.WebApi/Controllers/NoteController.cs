using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Notes;
using ToDoList.Domain.Entities;

namespace ToDoList.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Administrator, Manager")]
public class NoteController : ControllerBase
{
    private NoteLogic _logic;

    public NoteController(NoteLogic logic)
    {
        _logic = logic;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Note value)
    {
        try
        {
            return Ok(await _logic.CreateAsync(value));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
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
