using ToDoList.Application.Common.Interfaces;
using ToDoList.Domain.Entities;

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
