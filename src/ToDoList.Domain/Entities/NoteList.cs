using System.Drawing;

namespace ToDoList.Domain.Entities;

public class NoteList : IEquatable<NoteList>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public Color Color { get; set; } = Color.White;

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
        return HashCode.Combine(Id, Title, Color, Notes);
    }
}