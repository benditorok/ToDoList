using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ToDoList.Domain.Entities;

public class ToDoList : IEquatable<ToDoList>
{
    public int Id { get; set; }

    public virtual ICollection<ToDoItem> ToDoItems { get; set; } = new HashSet<ToDoItem>();

    public ToDoList()
    {
    }

    public ToDoList(int id, List<ToDoItem> toDoItems)
    {
        Id = id;
        ToDoItems = toDoItems;
    }

    public bool Equals(ToDoList? other)
    {
        return GetHashCode() == other?.GetHashCode();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
