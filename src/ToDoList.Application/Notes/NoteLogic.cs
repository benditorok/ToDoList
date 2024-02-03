using ToDoList.Application.Common.Interfaces;
using ToDoList.Domain.Constants;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exceptions;

namespace ToDoList.Application.Notes;

public class NoteLogic : ILogic<Note>
{
    private IRepository<Note> _repo;

    public NoteLogic(IRepository<Note> repo)
    {
        _repo = repo;
    }

    // TODO Data sanitization, logic
    public async Task<int> CreateAsync(Note item)
    {
        if (item.Title?.Length < NoteConstants.TilteMinLength || item.Title?.Length > NoteConstants.TilteMaxLength)
            throw new InvalidNoteException($"Title of note Id:{item.Id} is invalid.");

        if (item.Body?.Length < NoteConstants.BodyMinLength || item.Body?.Length > NoteConstants.BodyMaxLength)
            throw new InvalidNoteException($"Body of note Id:{item.Id} is invalid.");

        await _repo.CreateAsync(item);
        return item.Id;
    }

    public async Task<Note> ReadAsync(int id)
    {
        return await _repo.ReadAsync(id);
    }

    public async Task UpdateAsync(Note item)
    {
        await _repo.UpdateAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }

    public IQueryable<Note> ReadAll()
    {
        return _repo.ReadAll();
    }
}
