using ToDoList.Client.Views.Identity;
using ToDoList.Client.Views.Main;
using ToDoList.Client.Views.NoteLists;
using ToDoList.Client.Views.Notes;

namespace ToDoList.Client;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Routing registration
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

        // Identity
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));

        // Pages
        Routing.RegisterRoute(nameof(NoteListPage), typeof(NoteListPage));
        Routing.RegisterRoute(nameof(NoteSelectionPage), typeof(NoteSelectionPage));
        Routing.RegisterRoute(nameof(NoteEditorPage), typeof(NoteEditorPage));
    }
}
