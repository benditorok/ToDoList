namespace ToDoList.Client.Services.Connection;

public class ConnectionData
{
    public string BaseURL { get; set; }

    public string PingableEndpoint { get; set; }

    public ConnectionData(string baseURL, string pingableEndpoint)
    {
        BaseURL = baseURL;
        PingableEndpoint = pingableEndpoint;
    }
}
