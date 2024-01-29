using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using ToDoList.Client.Common;
using ToDoList.Client.Services.Connection;
using ToDoList.Client.Views.Identity;
using ToDoList.Client.Views.NoteLists;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.Main;

public partial class MainViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;
    private ILogger<MainViewModel>? _logger;
    private static SemaphoreSlim _sm = new SemaphoreSlim(1, 1);

    [ObservableProperty]
    private NoteList? _userToDoList;

    [ObservableProperty]
    private Note _userToDoItem = new();

    public MainViewModel(AuthorizedConnectionService connectionService, ILogger<MainViewModel>? logger)
    { 
        _connectionService = connectionService;
        _logger = logger;
    }

    public async void OnPageLoaded(object? sender, EventArgs e)
    {
        if (_connectionService.IsLoggedIn)
        {
            await LoadToDoListAsync();
        }
        else
        {
            await Shell.Current.GoToAsync("//AccountPage");
        }
    }

    private async Task LoadToDoListAsync()
    {
        try
        {
            UserToDoList = await _connectionService.GetAsync<NoteList>("user/gettodolist");
        }
        catch (Exception ex)
        {
            _logger?.LogInformation("[VM-MAIN] {ex}", ex.Message);

            if (await Shell.Current.DisplayAlert("Alert", "Loading the ToDoList failed!", "Retry", "Exit"))
            {
                await LoadToDoListAsync();
            }
            else
            {
                Application.Current?.Quit();
            }
        }
    }

    private async Task RefreshToDoListAsync()
    {
        try
        {
            var list = await _connectionService.GetAsync<NoteList>($"user/getnotelist?id={UserToDoList?.Id}");

            if (list != null)
            {
                // TODO sort server side
                list.Notes.OrderByDescending(x => x.Id).ThenByDescending(x => !x.IsDone);
                UserToDoList = list;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogInformation("[VM-MAIN] {ex}", ex.Message);
        }
    }

    [RelayCommand]
    private async Task QuickAddNoteAsync()
    {
        try
        {
            await _connectionService.PostAsync<Note>($"user/addnote?listId={UserToDoList?.Id}", UserToDoItem);
        }
        catch (Exception ex)
        {
            _logger?.LogInformation("[VM-MAIN] {ex}", ex.Message);
        }
        finally
        {
            UserToDoItem = new();
            await RefreshToDoListAsync();
        }
    }

    [RelayCommand] 
    private async Task SwitchNoteCompletionStateAsync(Note note)
    {
        try
        {
            bool done = note.IsDone;
            note.IsDone = !done;

            await _connectionService.PutAsync<Note>($"user/updatenote", note);
        }
        catch (Exception ex)
        {
            _logger?.LogInformation("[VM-MAIN] {ex}", ex.Message);
        }
        finally
        {
            await RefreshToDoListAsync();
        }
    }

    [RelayCommand]
    private async Task QuickRemoveNoteAsync(Note note)
    {
        try
        {
            if(await Shell.Current.DisplayAlert("Alert", $"Are you sure you want to delete this note?", "Yes", "Cancel"))
            {
                await _connectionService.DeleteAsync($"user/removenote?id={note?.Id}");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogInformation("[VM-MAIN] {ex}", ex.Message);
        }
        finally
        {
            await RefreshToDoListAsync();
        }
    }

    [RelayCommand]
    private async Task GotoNoteListPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(NoteListPage));
    }
}
