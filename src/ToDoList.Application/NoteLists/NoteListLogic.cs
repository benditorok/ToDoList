using ToDoList.Application.Common.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.NoteLists;

public class NoteListLogic : ILogic<NoteList>
{
    private IRepository<NoteList> _repo;

    public NoteListLogic(IRepository<NoteList> repo)
    {
        _repo = repo;
    }

    // TODO Data sanitization, logic
    public async Task<int> CreateAsync(NoteList item)
    {
        await _repo.CreateAsync(item);
        return item.Id;
    }

    public async Task<NoteList> ReadAsync(int id)
    {
        return await _repo.ReadAsync(id);
    }

    public async Task UpdateAsync(NoteList item)
    {
        await _repo.UpdateAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }

    public IQueryable<NoteList> ReadAll()
    {
        return _repo.ReadAll();
    }
}
