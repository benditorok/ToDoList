using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Common.Interfaces;
using ToDoList.Application.Notes;
using ToDoList.Domain.Entities;

namespace ToDoList.WebApi.Controllers;

/// <summary>
/// CRUD controller for notes only accessible by 'Administrator' and 'Manager'.
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Administrator, Manager")]
public class NoteController : ControllerBase
{
    private ILogic<Note> _logic;

    public NoteController(ILogic<Note> logic)
    {
        _logic = logic;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Note value)
    {
        int id = await _logic.CreateAsync(value);
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadAsync(int id)
    {
        var item = await _logic.ReadAsync(id);
        return Ok(item);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] Note value)
    {
        await _logic.UpdateAsync(value);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _logic.DeleteAsync(id);
        return Ok();
    }
}
