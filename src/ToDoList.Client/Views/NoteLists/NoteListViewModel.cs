using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Client.Services.Connection;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.NoteLists;

public partial class NoteListViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;

    [ObservableProperty]
    private List<NoteList> _userNoteLists = new();

    [ObservableProperty]
    private NoteList _newNoteList = new();

    // TODO add colors somehow
    [ObservableProperty]
    private List<string> _availableColors = new()
    {
        "Red",
        "Green",
        "Blue"
    };

    [ObservableProperty]
    private string? _selectedColor;

    public NoteListViewModel(AuthorizedConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    private async Task RefreshUserNotes()
    {
        UserNoteLists = await _connectionService.GetAsync<NoteList>("user/getallnotelists");
    }

    [RelayCommand]
    public async Task AddNoteList()
    {
        try
        {
            await _connectionService.PostAsync<NoteList>(NewNoteList, "user/addnotelist");
            await Shell.Current.DisplayAlert("Alert", "Creation was successful!", "OK");

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", "Creation failed!", "OK");
        }
        finally
        {
            await RefreshUserNotes();
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
