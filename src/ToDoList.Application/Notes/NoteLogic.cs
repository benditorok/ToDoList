using ToDoList.Application.Common.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Notes;

public class NoteLogic
{
    private IRepository<Note> _repo;

    public NoteLogic(IRepository<Note> repo)
    {
        _repo = repo;
    }

    // TODO Implement logic
    public async Task CreateAsync(Note item)
    {
        await _repo.CreateAsync(item);
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
}
