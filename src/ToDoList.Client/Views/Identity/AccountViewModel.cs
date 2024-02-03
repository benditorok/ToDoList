using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using ToDoList.Client.Services.Connection;
using ToDoList.Client.Views.Main;

namespace ToDoList.Client.Views.Identity;

public partial class AccountViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;
    private ILogger<AccountViewModel>? _logger;

    [ObservableProperty]
    private string? _userName = "";

    [ObservableProperty]
    private bool _isLoggedIn = false;

    [ObservableProperty]
    private bool _isLoggedInInverted = true;

    public AccountViewModel(AuthorizedConnectionService connectionService, ILogger<AccountViewModel>? logger) 
    {
        _connectionService = connectionService;
        _logger = logger;
    }

    public void OnPageLoaded(object? sender, EventArgs e)
    {
        // TODO make the inverted val another way
        if (_connectionService.IsLoggedIn)
        {
            UserName = _connectionService.UserName;
            IsLoggedIn = true;
            IsLoggedInInverted = false;
        }
        else
        {
            IsLoggedIn = false;
            IsLoggedInInverted = true;
        }
    }

    [RelayCommand]
    private async Task Logout()
    {
        _connectionService.LogoutClearToken();
        IsLoggedIn = false;
        IsLoggedInInverted = true;

        await Shell.Current.GoToAsync("//AccountPage");
    }

    [RelayCommand]
    private async Task GotoLoginPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    [RelayCommand]
    private async Task GotoMainPageAsync()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
