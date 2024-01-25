using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Client.Services.Connection;
using ToDoList.Client.Views.Notes;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.NoteLists;

[QueryProperty("NoteList", "UserNoteList")]
public partial class NoteSelectionViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;

    [ObservableProperty]
    private NoteList? _noteList;

    [ObservableProperty]
    private Note _newNote = new();

    // TODO page needs to be updated when it becomes visible
    public NoteSelectionViewModel(AuthorizedConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    public async void OnPageLoaded(object? sender, EventArgs e)
    {
        await RefreshUserNotesAsync();
    }

    [RelayCommand]
    private async Task AddNoteAsync()
    {
        try
        {
            await _connectionService.PostAsync<Note>($"user/addnote?listId={NoteList?.Id}", NewNote);
            await Shell.Current.DisplayAlert("Alert", "Creation was successful!", "OK");

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", "Creation failed!", "OK");
        }
        finally
        {
            NewNote = new();
            await RefreshUserNotesAsync();
        }
    }

    private async Task RefreshUserNotesAsync()
    {
        NoteList = await _connectionService.GetAsync<NoteList>($"user/getnotelist?id={NoteList?.Id}");
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task GotoNoteEditorAsync(Note note)
    {
        var navigationParameter = new ShellNavigationQueryParameters()
        {
            { "UserNote", note }
        };

        await Shell.Current.GoToAsync($"{nameof(NoteEditorPage)}", navigationParameter);
    }
}
