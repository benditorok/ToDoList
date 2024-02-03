namespace ToDoList.Domain.Exceptions;

public class InvalidNoteException : Exception
{
    public InvalidNoteException(string message) : base(message)
    {
    }
}
