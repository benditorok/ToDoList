using Microsoft.Extensions.Logging;
using ToDoList.Client.Services.Connection;
using ToDoList.Client.Views;

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

#if ANDROID
        builder.Services.AddSingleton<ConnectionData>(_ => new("http://10.0.2.2:8080/", "Status"));
#else
        builder.Services.AddSingleton<ConnectionData>(_ => new("http://localhost:8080/", "Status"));
#endif

        builder.Services.AddSingleton<AuthorizedConnectionService>();

        builder.Services
            .AddViews();

        return builder.Build();
    }
}
