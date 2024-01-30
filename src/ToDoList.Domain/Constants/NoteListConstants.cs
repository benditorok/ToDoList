namespace ToDoList.Domain.Constants;

public class NoteListConstants
{
    public static readonly int TitleMinLength = 1;

    public static readonly int TitleMaxLength = 50;

    public static readonly int ColorRGBALength = 9;

    public static readonly char ColorRGBAStartsWith = '#';

    public static readonly char[] ColorRGBAChars =
    {
        '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'A', 'B', 'C', 'D', 'E', 'F',
        'a', 'b', 'c', 'd', 'e', 'f',
        '#'
    };
}
