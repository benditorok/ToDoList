using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using ToDoList.Client.Services.Connection;
using ToDoList.Client.Views.Notes;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.NoteLists;

[QueryProperty("NoteList", "UserNoteList")]
public partial class NoteSelectionViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;
    private ILogger<NoteSelectionViewModel>? _logger;

    [ObservableProperty]
    private NoteList? _noteList;

    [ObservableProperty]
    private Note _newNote = new();

    public NoteSelectionViewModel(AuthorizedConnectionService connectionService, ILogger<NoteSelectionViewModel>? logger)
    {
        _connectionService = connectionService;
        _logger = logger;
    }

    public async void OnPageLoaded(object? sender, EventArgs e)
    {
        await RefreshUserNotesAsync();
    }

    private async Task RefreshUserNotesAsync()
    {
        NoteList = await _connectionService.GetAsync<NoteList>($"user/getnotelist?id={NoteList?.Id}");
        _logger?.LogInformation("Refreshed user notes.");
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
            _logger?.LogError(ex, ex.Message);
        }
        finally
        {
            NewNote = new();
            await RefreshUserNotesAsync();
        }
    }

    [RelayCommand]
    private async Task RemoveNoteAsync(Note note)
    {
        try
        {
            if (await Shell.Current.DisplayAlert("Alert", $"Are you sure you want to delete this note? {note.Title}", "Yes", "Cancel"))
            {
                await _connectionService.DeleteAsync($"user/removenote?id={note.Id}");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, ex.Message);
        }
        finally
        {
            await RefreshUserNotesAsync();
        }
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
