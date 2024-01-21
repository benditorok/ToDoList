using ToDoList.Application.Common.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.NoteLists;

public class NoteListLogic
{
    private IRepository<NoteList> _repo;

    public NoteListLogic(IRepository<NoteList> repo)
    {
        _repo = repo;
    }

    // TODO Implement logic
    public async Task CreateAsync(NoteList item)
    {
        await _repo.CreateAsync(item);
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
}
