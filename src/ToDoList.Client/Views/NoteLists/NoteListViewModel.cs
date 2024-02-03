using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using ToDoList.Client.Common;
using ToDoList.Client.Services.Connection;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.NoteLists;

public partial class NoteListViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;
    private ILogger<NoteListViewModel>? _logger;

    [ObservableProperty]
    private List<NoteList>? _userNoteLists;

    [ObservableProperty]
    private NoteList _newNoteList = new();

    [ObservableProperty]
    private ObservableCollection<ColorInfo> _availableColors = new()
    {
        new ColorInfo("Red", "#FF00005F"),
        new ColorInfo("Green", "#00FF005F"),
        new ColorInfo("Blue", "#0000FF5F"),
        new ColorInfo("White", "#FFFFFF5F"),
    };

    [ObservableProperty]
    private ColorInfo? _selectedColor;

    public NoteListViewModel(AuthorizedConnectionService connectionService, ILogger<NoteListViewModel>? logger)
    {
        _connectionService = connectionService;
        _logger = logger;
    }

    public async void OnPageLoaded(object? sender, EventArgs e)
    {
        await RefreshUserNoteListsAsync();
    }

    private async Task RefreshUserNoteListsAsync()
    {
        UserNoteLists = await _connectionService.GetAsync<List<NoteList>>("user/getallnotelists");
        _logger?.LogInformation("Refreshed user notelists.");
    }

    [RelayCommand]
    public async Task AddNoteListAsync()
    {
        try
        {
            NewNoteList.ColorRGBA = SelectedColor?.RGBA ?? "#FFFFFF5F";
            await _connectionService.PostAsync<NoteList>("user/addnotelist", NewNoteList);
            await Shell.Current.DisplayAlert("Alert", "Creation was successful!", "OK");

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", "Creation failed!", "OK");
            _logger?.LogError(ex, ex.Message);
        }
        finally
        {
            NewNoteList = new();
            await RefreshUserNoteListsAsync();
        }
    }

    [RelayCommand]
    public async Task GotoNoteSelectionAsync(NoteList noteList)
    {
        var navigationParameter = new ShellNavigationQueryParameters()
        {
            { "UserNoteList", noteList }
        };
    
        await Shell.Current.GoToAsync($"{nameof(NoteSelectionPage)}", navigationParameter);
    }

    [RelayCommand]
    private async Task RemoveNoteListAsync(NoteList noteList)
    {
        try
        {
            if (await Shell.Current.DisplayAlert("Alert", $"Are you sure you want to delete this list? {noteList?.Title}", "Yes", "Cancel"))
            {
                await _connectionService.DeleteAsync($"user/removenotelist?id={noteList?.Id}");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, ex.Message);
        }
        finally
        {
            await RefreshUserNoteListsAsync();
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
