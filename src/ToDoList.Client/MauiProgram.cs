using Microsoft.Extensions.Logging;
using ToDoList.Client.Services.Connection;

namespace ToDoList.Client;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.SetMinimumLevel(LogLevel.Information);
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<ConnectionData>(_ => new("http://localhost:8080/", "Status"));
        builder.Services.AddSingleton<AuthorizedConnectionService>();

        return builder.Build();
    }
}
