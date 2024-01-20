using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entities.ToDo;

public class Note : IEquatable<Note>
{
    public int Id { get; set; }

    public string Creator { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public bool IsDone { get; set; } = false;

    public Note()
    {
    }

    public Note(string creator, string title, string body, bool isDone)
    {
        Creator = creator;
        Title = title;
        Body = body;
        IsDone = isDone;
    }

    public bool Equals(Note? other)
    {
        return GetHashCode() == other?.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Creator, Title, Body, IsDone);
    }
}
