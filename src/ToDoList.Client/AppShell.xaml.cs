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

        // Identity
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        // Pages
        Routing.RegisterRoute(nameof(NoteListPage), typeof(NoteListPage));
        Routing.RegisterRoute(nameof(NoteSelectionPage), typeof(NoteSelectionPage));
        Routing.RegisterRoute(nameof(NoteEditorPage), typeof(NoteEditorPage));
    }
}
