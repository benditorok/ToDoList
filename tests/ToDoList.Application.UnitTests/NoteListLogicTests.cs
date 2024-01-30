using NUnit.Framework;
using ToDoList.Domain.Constants;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exceptions;

namespace ToDoList.Application.UnitTests;

[TestFixture]
public class NoteListLogicTests : MockContext
{
    [SetUp]
    public override void Setup() => base.Setup();


    private static char[][] _validTitle =
    {
        new char[NoteListConstants.TilteMaxLength],
        new char[NoteListConstants.TilteMinLength]
    };

    private static char[][] _invalidTitle =
    {
        new char[NoteListConstants.TilteMaxLength + 1],
        new char[NoteListConstants.TilteMinLength >= 1 ? NoteConstants.TilteMinLength - 1 : 0]
    };

    private static string[] _validColorRGBA =
    {
        "#FFFFFFFF",
        "#A8B2C3D4"
    };

    private static string[] _invalidColorRGBA =
    {
        "#FFFFFFF",
        "#A8B2C3D45",
        "A8B2C3D45",
        "#FFFFFFFX"
    };

    [Test]
    [TestCaseSource(nameof(_validTitle))]
    public void Create_Valid_Title_Async(char[] title)
    {
        title = Enumerable.Repeat('s', title.Length).ToArray();

        var item = new NoteList() { Title = new string(title) };

        Assert.DoesNotThrowAsync(async () => await _noteListLogic.CreateAsync(item));
    }

    [Test]
    [TestCaseSource(nameof(_invalidTitle))]
    public void Create_Invalid_Title_Async(char[] title)
    {
        title = Enumerable.Repeat('s', title.Length).ToArray();

        var item = new NoteList() { Title = new string(title) };

        Assert.ThrowsAsync<InvalidNoteListException>(async () => await _noteListLogic.CreateAsync(item));
    }

    [Test]
    [TestCaseSource(nameof(_validColorRGBA))]
    public void Create_Valid_ColorRGBA_Async(string color)
    {
        var title = Enumerable.Repeat('s', _validTitle[0].Length).ToArray();

        var item = new NoteList() { Title = new string(title), ColorRGBA = color };

        Assert.DoesNotThrowAsync(async () => await _noteListLogic.CreateAsync(item));
    }

    [Test]
    [TestCaseSource(nameof(_invalidColorRGBA))]
    public void Create_Invalid_ColorRGBA_Async(string color)
    {
        var title = Enumerable.Repeat('s', _validTitle[0].Length).ToArray();

        var item = new NoteList() { Title = new string(title), ColorRGBA = color };

        Assert.ThrowsAsync<InvalidNoteListException>(async () => await _noteListLogic.CreateAsync(item));
    }
}
