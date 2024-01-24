using ToDoList.Client.Views.Identity;
using ToDoList.Client.Views.NoteLists;

namespace ToDoList.Client;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Routing registration
        // Identity
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(NoteListPage), typeof(NoteListPage));
    }
}
