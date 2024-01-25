using System.Drawing;

namespace ToDoList.Domain.Entities;

public class NoteList : IEquatable<NoteList>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string ColorRGBA { get; set; } = "#FFFFFF3F";

    public string? UserId { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new HashSet<Note>();

    public NoteList()
    {
    }

    public bool Equals(NoteList? other)
    {
        return GetHashCode() == other?.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, ColorRGBA, UserId, Notes);
    }
}