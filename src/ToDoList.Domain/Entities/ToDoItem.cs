using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entities;

public class ToDoItem : IEquatable<ToDoItem>
{
    public int Id { get; set; }

    public string Creator { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public bool IsDone { get; set; } = false;

    public int ToDoListId { get; set; }

    public virtual ToDoList? ToDoList { get; set; }

    public ToDoItem()
    {
    }

    public ToDoItem(int id, string creator, string title, string body, bool isDone, int toDoListId)
    {
        Id = id;
        Creator = creator;
        Title = title;
        Body = body;
        IsDone = isDone;
        ToDoListId = toDoListId;
    }

    public bool Equals(ToDoItem? other)
    {
        return GetHashCode() == other?.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Creator, Title, Body, IsDone);
    }
}
