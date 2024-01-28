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
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainPage>();

        services.AddTransient<AccountViewModel>();
        services.AddTransient<AccountPage>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<LoginPage>();

        services.AddTransient<RegisterViewModel>();
        services.AddTransient<RegisterPage>();

        services.AddTransient<NoteListViewModel>();
        services.AddTransient<NoteListPage>();

        services.AddTransient<NoteSelectionViewModel>();
        services.AddTransient<NoteSelectionPage>();

        services.AddTransient<NoteEditorViewModel>();
        services.AddTransient<NoteEditorPage>();

        return services;
    }
}
