using System.Text.Json.Serialization;

namespace ToDoList.Domain.Entities;

public class Note : IEquatable<Note>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public bool IsDone { get; set; } = false;

    public DateTime? Reminder { get; set; }

    public int NoteListId { get; set; }

    [JsonIgnore]
    public virtual NoteList? NoteList { get; set; }

    public Note()
    {
    }

    public bool Equals(Note? other)
    {
        return GetHashCode() == other?.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, Body, IsDone, Reminder, NoteListId);
    }
}