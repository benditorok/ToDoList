using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using ToDoList.Application.Common.Interfaces;
using ToDoList.Application.NoteLists;
using ToDoList.Application.Notes;
using ToDoList.Domain.Entities;

namespace ToDoList.WebApi.Controllers;

[Route("user")]
[ApiController]
[Authorize]
public class UserInteractionController : ControllerBase
{
    private NoteListLogic _noteListLogic;
    private NoteLogic _noteLogic;

    public UserInteractionController(NoteListLogic noteListLogic, NoteLogic noteLogic)
    {
        _noteListLogic = noteListLogic;
        _noteLogic = noteLogic;
    }

    [HttpPost("addnotelist")]
    public async Task<IActionResult> AddNoteListAsync([FromBody] NoteList value)
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (user == null)
        {
            return Forbid();
        }

        value.UserId = user.Value;
        await _noteListLogic.CreateAsync(value);

        return Ok(value.Id);
    }

    [HttpGet("getallnotelists")]
    public async Task<IActionResult> GetAllNoteListAsync()
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
            return Forbid();
        }

        return Ok(await _noteListLogic.ReadAll()
                .Where(x => x.UserId == user.Value)
                .OrderByDescending(x => x.Id)
                .ToListAsync());
    }

    [HttpGet("getnotelist")]
    public async Task<IActionResult> GetNoteListAsync(int id)
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var item = await _noteListLogic.ReadAsync(id);

        if (item?.UserId == user?.Value)
        {
            return Ok(item);
        }
        else
        {
            return Forbid();
        }
    }

    [HttpGet("gettodolist")]
    public async Task<IActionResult> GetToDoListAsync()
    {
        // Name of the user's todo list.
        // If the user deletes it this will create a new one and assign it to the user.
        const string title = "ToDoList";

        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (user == null)
        {
            return Forbid();
        }

        var item = (user != null) ? await _noteListLogic.ReadAll()
            .FirstOrDefaultAsync(x => x.UserId == user.Value && x.Title == title) : null;

        if (item == null)
        {
            int todolistId = await _noteListLogic.CreateAsync(new() { Title = title, UserId = user?.Value });
            item = await _noteListLogic.ReadAsync(todolistId);
        }

        return Ok(item);
    }

    [HttpPut("updatenotelist")]
    public async Task<IActionResult> UpdateNoteListAsync([FromBody] NoteList value)
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var old = await _noteListLogic.ReadAsync(value.Id);

        if (old.UserId == user?.Value && old.UserId == value.UserId)
        {
            await _noteListLogic.UpdateAsync(value);
            return Ok();
        }
        else
        {
            return Forbid();
        }
    }

    [HttpDelete("removenotelist")]
    public async Task<IActionResult> DeleteNoteListAsync(int id)
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var item = await _noteListLogic.ReadAsync(id);

        if (user?.Value == item.UserId)
        {
            await _noteListLogic.DeleteAsync(id);
            return Ok();
        }
        else
        {
            return Forbid();
        }
    }

    [HttpPost("addnote")]
    public async Task<IActionResult> AddNoteAsync(int listId, [FromBody] Note value)
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var list = await _noteListLogic.ReadAsync(listId);

        if (list.UserId == user?.Value)
        {
            value.NoteListId = listId;
            await _noteLogic.CreateAsync(value);
            return Ok(value.Id);
        }
        else
        {
            return Forbid();
        }
    }

    [HttpPut("updatenote")]
    public async Task<IActionResult> UpdateNoteAsync([FromBody] Note value)
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var list = await _noteListLogic.ReadAsync(value.NoteListId);

        if (list.UserId == user?.Value)
        {
            if (list.Notes.Any(x => x.Id == value.Id))
            {
                await _noteLogic.UpdateAsync(value);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return Forbid();
        }
    }

    [HttpDelete("removenote")]
    public async Task<IActionResult> RemoveNoteAsync(int id)
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var note = await _noteLogic.ReadAsync(id);

        if (note.NoteList?.UserId == user?.Value)
        {
            await _noteLogic.DeleteAsync(id);
            return Ok();
        }
        else
        {
            return Forbid();
        }
    }

    [HttpGet("getuserinfo")]
    public Task<IActionResult> GetUserInfoAsync()
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (user != null)
        {
            return Task.FromResult<IActionResult>(Ok(user.Value));
        }
        else
        {
            return Task.FromResult<IActionResult>(Forbid());
        }
    }
}
