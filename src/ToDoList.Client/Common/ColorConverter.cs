using System.Globalization;

namespace ToDoList.Client.Common;

public class ColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Color color;

        if (value is string colorRGBA)
        {
            // Parse the RGBA string and create a Color
            if (Color.FromRgba(colorRGBA) != null)
            {
                color = Color.FromRgba(colorRGBA);
                return color;
            }
        }

        // Return a default color if parsing fails
        return default(Color);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
