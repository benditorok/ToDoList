using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using ToDoList.Client.Services.Connection;
using ToDoList.Client.Views.Identity;
using ToDoList.Client.Views.NoteLists;

namespace ToDoList.Client.Views.Main;

public partial class MainViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;
    private ILogger<MainViewModel>? _logger;

    public MainViewModel(AuthorizedConnectionService connectionService, ILogger<MainViewModel>? logger)
    { 
        _connectionService = connectionService;
        _logger = logger;
    }

    public async void OnPageLoaded(object? sender, EventArgs e)
    {
        if (_connectionService.IsLoggedIn)
        {
            await Shell.Current.GoToAsync(nameof(NoteListPage));
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(AccountPage));
        }
    }


     // TODO make the default page a simple todolist and add a nav to the notelists page
     // todolist should be the first notelist of the user or something like that
}
