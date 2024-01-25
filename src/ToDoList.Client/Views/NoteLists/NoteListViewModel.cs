using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoList.Client.Common;
using ToDoList.Client.Services.Connection;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.NoteLists;

public partial class NoteListViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;

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

    public NoteListViewModel(AuthorizedConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    private async Task RefreshUserNotes()
    {
        UserNoteLists = await _connectionService.GetAsync<List<NoteList>>("user/getallnotelists");
    }

    [RelayCommand]
    public async Task AddNoteList()
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
        }
        finally
        {
            NewNoteList = new();
            await RefreshUserNotes();
        }
    }

    [RelayCommand]
    public async Task RemoveNoteList(NoteList noteList)
    {
        try
        {
            await _connectionService.DeleteAsync($"user/removenotelist?id={noteList.Id}");
            await Shell.Current.DisplayAlert("Alert", "Removal was successful!", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", "Removal failed!", "OK");
        }
        finally
        {
            NewNoteList = new();
            await RefreshUserNotes();
        }
    }

    [RelayCommand]
    public async Task GotoNoteSelection(NoteList noteList)
    {
        var navigationParameter = new ShellNavigationQueryParameters()
        {
            { "NoteList", noteList }
        };
    
        await Shell.Current.GoToAsync($"{nameof(NoteSelectionPage)}", navigationParameter);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
