using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToDoList.Client.Views.Main;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    { 
    }

    [RelayCommand]
    private async Task GotoLoginPageAsync()
    {
        await Shell.Current.GoToAsync("LoginPage");
    }

    [RelayCommand]
    private async Task GotoRegisterPageAsync()
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }

    [RelayCommand]
    private async Task GotoNoteListPageAsync()
    {
        await Shell.Current.GoToAsync("NoteListPage");
    }
}
