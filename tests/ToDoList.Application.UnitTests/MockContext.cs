using MockQueryable.Moq;
using Moq;
using ToDoList.Application.Common.Interfaces;
using ToDoList.Application.NoteLists;
using ToDoList.Application.Notes;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.UnitTests;

public class MockContext
{
    protected NoteLogic _noteLogic = null!;
    protected NoteListLogic _noteListLogic = null!;

    private static Mock<IRepository<Note>> _noteRepository = null!;
    private static Mock<IRepository<NoteList>> _noteListRepository = null!;

    private static List<Note> _notes = new()
    {
        new Note()
        {
            Id = 1,
            Title = "First",
            NoteListId = 1,
        },
        new Note()
        {
            Id = 2,
            Title = "Second",
            NoteListId = 2,
        },
        new Note()
        {
            Id = 3,
            Title = "Third",
            NoteListId = 2,
        }
    };

    private static List<NoteList> _noteLists = new()
    {
        // Has 1 note
        new NoteList()
        {
            Id = 1,
            Title = "First list",
        },
        // Has 2 notes
        new NoteList()
        {
            Id = 1,
            Title = "Second list",
        },
        // Empty
        new NoteList()
        {
            Id = 1,
            Title = "Third list",
        }
    };

    public virtual void Setup()
    {
        _noteRepository = new();
        _noteListRepository = new();

        // Navigation properties
        _notes.ForEach(note => note.NoteList = _noteLists.Find(notelist => note.NoteListId == notelist.Id));
        _noteLists.ForEach(notelist => notelist.Notes = _notes.FindAll(note => notelist.Id == note.NoteListId));

        // Setup mocks
        var notesMock = _notes.BuildMock();
        var noteListMock = _noteLists.BuildMock();

        _noteRepository.Setup(x => x.ReadAll()).Returns(notesMock);
        _noteListRepository.Setup(x => x.ReadAll()).Returns(noteListMock);

        // Setup logic
        _noteLogic = new NoteLogic(_noteRepository.Object);
        _noteListLogic = new NoteListLogic(_noteListRepository.Object);
    }
}
