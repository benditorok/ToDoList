using ToDoList.Application.Common.Interfaces;
using ToDoList.Domain.Constants;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exceptions;

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
        if (item.Title?.Length < NoteListConstants.TitleMinLength || 
            item.Title?.Length > NoteListConstants.TitleMaxLength)
            throw new InvalidNoteListException($"Title of notelist Id:{item.Id} is invalid.");

        if (item.ColorRGBA.Length != NoteListConstants.ColorRGBALength || 
            !item.ColorRGBA.StartsWith(NoteListConstants.ColorRGBAStartsWith) || 
            !item.ColorRGBA.All(x => NoteListConstants.ColorRGBAChars.Contains(x)))
            throw new InvalidNoteListException($"ColorRGBA of notelist Id:{item.Id} is invalid.");
         
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
