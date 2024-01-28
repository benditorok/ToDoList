using Microsoft.Extensions.Logging;

namespace ToDoList.Client.Common;

public class ColorInfo
{
    public string? Name { get; set; }

    public string? RGBA { get; set; }

    public ColorInfo()
    {
    }

    public ColorInfo(string? name, string? RGBA)
    {
        Name = name;
        this.RGBA = RGBA;
    }

    public Color? GetColorFromRGBA()
    {
        return Color.FromRgba(RGBA) ?? default(Color);
    }

    public override string ToString()
    {
        return Name ?? "Default";
    }
}
