using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using ToDoList.Application.NoteLists;
using ToDoList.Domain.Entities;

namespace ToDoList.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Administrator, Manager")]
public class NoteListController : ControllerBase
{
    private NoteListLogic _logic;

    public NoteListController(NoteListLogic logic)
    {
        _logic = logic;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] NoteList value)
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
    public async Task<IActionResult> UpdateAsync([FromBody] NoteList value)
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
