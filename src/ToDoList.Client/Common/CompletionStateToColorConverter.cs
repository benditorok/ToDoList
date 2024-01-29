using System.Globalization;

namespace ToDoList.Client.Common;

public class CompletionStateToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isDone)
        {
            if (isDone)
            {
                return Color.FromRgba("#00FF005F");
            }
            else
            {
                return Color.FromRgba("#FF00005F");
            }
        }

        return default(Color);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
