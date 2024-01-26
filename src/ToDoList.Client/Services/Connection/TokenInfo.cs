namespace ToDoList.Client.Services.Connection;

public class TokenInfo
{
    public string? TokenType { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set;}

    public int ExpiresIn { get; set; } = int.MaxValue;
}
