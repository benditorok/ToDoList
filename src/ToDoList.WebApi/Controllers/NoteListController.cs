using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using ToDoList.Application.Common.Interfaces;
using ToDoList.Application.NoteLists;
using ToDoList.Domain.Entities;

namespace ToDoList.WebApi.Controllers;

/// <summary>
/// CRUD controller for notelists only accessible by 'Administrator' and 'Manager'.
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Administrator, Manager")]
public class NoteListController : ControllerBase
{
    private ILogic<NoteList> _logic;

    public NoteListController(ILogic<NoteList> logic)
    {
        _logic = logic;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] NoteList value)
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
    public async Task<IActionResult> UpdateAsync([FromBody] NoteList value)
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
