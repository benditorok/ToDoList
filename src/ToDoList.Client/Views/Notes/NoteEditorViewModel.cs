using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Client.Services.Connection;
using ToDoList.Domain.Entities;

namespace ToDoList.Client.Views.Notes;

[QueryProperty("Note", "UserNote")]
public partial class NoteEditorViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;

    [ObservableProperty]
    private Note? _note;

    public NoteEditorViewModel(AuthorizedConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    [RelayCommand]
    private async Task UpdateNoteAsync()
    {
        try
        {
            await _connectionService.PutAsync<Note>("user/updatenote", this.Note!);
            await Shell.Current.DisplayAlert("Alert", "Saving was successful!", "OK");
            await GoBackAsync();

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", "Saving failed!", "OK");
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
