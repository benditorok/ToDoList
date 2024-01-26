using Microsoft.Extensions.DependencyInjection;
using ToDoList.Client.Views.Identity;
using ToDoList.Client.Views.Main;
using ToDoList.Client.Views.NoteLists;
using ToDoList.Client.Views.Notes;

namespace ToDoList.Client.Views;

public static class DependencyInjection
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainPage>();

        services.AddSingleton<AccountViewModel>();
        services.AddSingleton<AccountPage>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<LoginPage>();

        services.AddTransient<RegisterViewModel>();
        services.AddTransient<RegisterPage>();

        services.AddSingleton<NoteListViewModel>();
        services.AddSingleton<NoteListPage>();

        services.AddTransient<NoteSelectionViewModel>();
        services.AddTransient<NoteSelectionPage>();

        services.AddTransient<NoteEditorViewModel>();
        services.AddTransient<NoteEditorPage>();

        return services;
    }
}
