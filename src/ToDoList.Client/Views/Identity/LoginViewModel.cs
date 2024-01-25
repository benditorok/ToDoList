using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using ToDoList.Client.Services.Connection;

namespace ToDoList.Client.Views.Identity;

public partial class LoginViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;
    private ILogger<LoginViewModel>? _logger;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _message;

    public LoginViewModel(AuthorizedConnectionService connectionService, ILogger<LoginViewModel>? logger)
    {
        _connectionService = connectionService;
        _logger = logger;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Username) ||
                !string.IsNullOrWhiteSpace(Password))
            {
                await _connectionService.LoginAsync(Username!, Password!);
                await Shell.Current.DisplayAlert("Alert", "Login was successful!", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", "Login failed!!", "OK");
            _logger?.LogInformation("[VM-LOGIN-EX] {ex}", ex.Message);
            Message = ex.Message;
        }
        finally
        {
            Username = null;
            Password = null;
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}