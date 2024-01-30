namespace ToDoList.Domain.Exceptions;

public class InvalidNoteListException : Exception
{
    public InvalidNoteListException(string message) : base(message)
    {
    }
}
