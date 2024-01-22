using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Client.Services.Connection;

namespace ToDoList.Client.Views.Identity;

public partial class LoginViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _message;

    public LoginViewModel(AuthorizedConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    [RelayCommand]
    private async Task Login()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Username) ||
                !string.IsNullOrWhiteSpace(Password))
            {
                await _connectionService.LoginAsync(Username!, Password!);
                await Shell.Current.DisplayAlert("Alert", "Login was successful!", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", "Login failed!!", "OK");
            Message = ex.Message;
        }
        finally
        {
            Username = null;
            Password = null;
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}