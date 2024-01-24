using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using ToDoList.Client.Services.Connection;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.NoteLists;

[QueryProperty("_noteList", "NoteList")]
public partial class NoteSelectionViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;

    [ObservableProperty]
    private int _noteListId;

    [ObservableProperty]
    private NoteList _noteList = new();

    public NoteSelectionViewModel(AuthorizedConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
