using CommunityToolkit.Mvvm.ComponentModel;
using ToDoList.Client.Services.Connection;

namespace ToDoList.Client.Views.Notes;

public partial class NoteEditorViewModel : ObservableObject
{
    private AuthorizedConnectionService _connectionService;

    public NoteEditorViewModel(AuthorizedConnectionService connectionService)
    {
        _connectionService = connectionService;
    }
}
