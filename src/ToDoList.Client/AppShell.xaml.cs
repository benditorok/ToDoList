using ToDoList.Client.Views.Identity;

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
    }
}
