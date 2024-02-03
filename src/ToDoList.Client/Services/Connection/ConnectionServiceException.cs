namespace ToDoList.Client.Services.Connection;

public class ConnectionServiceException : Exception
{
    public ConnectionServiceException(string? message) : base(message)
    {
    }
}
