using NUnit.Framework;
using ToDoList.Domain.Constants;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exceptions;

namespace ToDoList.Application.UnitTests;

[TestFixture]
public class NoteLogicTests : MockContext
{
    [SetUp]
    public override void Setup() => base.Setup();

    private static char[][] _validTitle =
{
        new char[NoteConstants.TilteMaxLength],
        new char[NoteConstants.TilteMinLength]
    };

    private static char[][] _invalidTitle =
    {
        new char[NoteConstants.TilteMaxLength + 1],
        new char[NoteConstants.TilteMinLength >= 1 ? NoteConstants.TilteMinLength - 1 : 0]
    };

    private static char[][] _validBody =
    {
        new char[NoteConstants.BodyMinLength],
        new char[NoteConstants.BodyMaxLength]
    };

    private static char[][] _invalidBody =
    {
        new char[NoteConstants.BodyMaxLength + 1],
        // Body can be empty
        //new char[NoteConstants.BodyMinLength >= 1 ? NoteConstants.BodyMinLength - 1 : 0]
    };

    [Test]
    [TestCaseSource(nameof(_validTitle))]
    public void Create_Valid_Title_Async(char[] title)
    {
        title = Enumerable.Repeat('s', title.Length).ToArray();
        char[] body = Enumerable.Repeat('s', _validBody[0].Length).ToArray();

        var item = new Note() { Title = new string(title), Body = new string(body) };

        Assert.DoesNotThrowAsync(async () => await _noteLogic.CreateAsync(item));
    }

    [Test]
    [TestCaseSource(nameof(_invalidTitle))]
    public void Create_Invalid_Title_Async(char[] title)
    {
        title = Enumerable.Repeat('s', title.Length).ToArray();
        char[] body = Enumerable.Repeat('s', _validBody[0].Length).ToArray();

        var item = new Note() { Title = new string(title), Body = new string(body) };

        Assert.ThrowsAsync<InvalidNoteException>(async () => await _noteLogic.CreateAsync(item));
    }

    [Test]
    [TestCaseSource(nameof(_validBody))]
    public void Create_Valid_Body_Async(char[] body)
    {
        char[] title = Enumerable.Repeat('s', _validTitle[0].Length).ToArray();
        body = Enumerable.Repeat('s', body.Length).ToArray();

        var item = new Note() { Title = new string(title), Body = new string(body) };

        Assert.DoesNotThrowAsync(async () => await _noteLogic.CreateAsync(item));
    }

    [Test]
    [TestCaseSource(nameof(_invalidBody))]
    public void Create_Invalid_Body_Async(char[] body)
    {
        char[] title = Enumerable.Repeat('s', _validTitle[0].Length).ToArray();
        body = Enumerable.Repeat('s', body.Length).ToArray();

        var item = new Note() { Title = new string(title), Body = new string(body) };

        Assert.ThrowsAsync<InvalidNoteException>(async () => await _noteLogic.CreateAsync(item));
    }
}