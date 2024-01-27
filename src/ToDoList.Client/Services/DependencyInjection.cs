using ToDoList.Client.Services.Connection;

namespace ToDoList.Client.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // TODO move to https
#if DEBUG

#if ANDROID
        services.AddSingleton<ConnectionData>(_ => new("http://10.0.2.2:8080/", "Status"));
#else
        services.AddSingleton<ConnectionData>(_ => new("http://localhost:8080/", "Status"));
#endif
#else
throw new Exception();
#endif
        services.AddTransient<TokenInfo>();
        services.AddSingleton<AuthorizedConnectionService>();

        return services;
    }
}
