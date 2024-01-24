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

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private NoteListLogic _noteListLogic;
    private NoteLogic _noteLogic;

    public UserController(NoteListLogic noteListLogic, NoteLogic noteLogic)
    {
        _noteListLogic = noteListLogic;
        _noteLogic = noteLogic;
    }

    [HttpPost("addnotelist")]
    public async Task<IActionResult> AddNoteListAsync([FromBody] NoteList value)
    {
        try
        {
            var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (user != null)
            {
                value.UserId = user.Value;
                await _noteListLogic.CreateAsync(value);

                return Ok(value.Id);
            }
            else
            {
                return Forbid();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getallnotelist")]
    public async Task<IActionResult> GetAllNoteListAsync()
    {
        try
        {
            var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (user != null)
            {
                return Ok(await _noteListLogic.ReadAll()
                    .Where(x => x.UserId == user.Value)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync());
            }
            else
            {
                return Forbid();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getnotelist")]
    public async Task<IActionResult> GetNoteListAsync(int id)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updatenotelist")]
    public async Task<IActionResult> UpdateNoteListAsync([FromBody] NoteList value)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("removenotelist")]
    public async Task<IActionResult> DeleteNoteListAsync(int id)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addnote")]
    public async Task<IActionResult> AddNoteAsync(int listId, [FromBody] Note value)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updatenote")]
    public async Task<IActionResult> UpdateNoteAsync([FromBody] Note value)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("removenote")]
    public async Task<IActionResult> RemoveNoteAsync(int id)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
