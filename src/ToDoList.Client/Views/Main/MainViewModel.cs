using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToDoList.Client.Views.Main;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    { 
    }

    [RelayCommand]
    private async Task GotoLoginPage()
    {
        await Shell.Current.GoToAsync("LoginPage");
    }

    [RelayCommand]
    private async Task GotoRegisterPage()
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }

    [RelayCommand]
    private async Task GotoNoteListPage()
    {
        await Shell.Current.GoToAsync("NoteListPage");
    }
}
