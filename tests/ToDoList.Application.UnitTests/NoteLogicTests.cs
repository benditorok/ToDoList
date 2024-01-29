using NUnit.Framework;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.UnitTests;

[TestFixture]
public class NoteLogicTests : MockContext
{
    private static Note[] _validNotes = { };

    [SetUp]
    public override void Setup() => base.Setup();

    [Test]
    [TestCase(nameof(_validNotes))]
    public async Task CreateValidAsync(Note note)
    {
        await _noteLogic.CreateAsync(note);
    }
}
