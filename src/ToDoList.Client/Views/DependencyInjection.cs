using Microsoft.Extensions.DependencyInjection;
using ToDoList.Client.Views.Identity;
using ToDoList.Client.Views.Main;

namespace ToDoList.Client.Views;

public static class DependencyInjection
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainPage>();

        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<LoginPage>();

        services.AddSingleton<RegisterViewModel>();
        services.AddSingleton<RegisterPage>();

        return services;
    }
}
