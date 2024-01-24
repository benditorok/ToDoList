using Microsoft.Extensions.DependencyInjection;
using ToDoList.Client.Views.Identity;
using ToDoList.Client.Views.Main;
using ToDoList.Client.Views.NoteLists;

namespace ToDoList.Client.Views;

public static class DependencyInjection
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainPage>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<LoginPage>();

        services.AddTransient<RegisterViewModel>();
        services.AddTransient<RegisterPage>();

        services.AddSingleton<NoteListViewModel>();
        services.AddSingleton<NoteListPage>();

        return services;
    }
}
