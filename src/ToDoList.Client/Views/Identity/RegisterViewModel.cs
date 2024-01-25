using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Client.Services.Connection;

namespace ToDoList.Client.Views.Identity;

public partial class RegisterViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _message;

    public RegisterViewModel(AuthorizedConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Username) ||
                !string.IsNullOrWhiteSpace(Password))
            {
                await _connectionService.RegisterAsync(Username!, Password!);
                await Shell.Current.DisplayAlert("Alert", "Registration was successful!", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", "Registration failed!", "OK");
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
