namespace ToDoList.Client.Views.Notes;

public partial class NoteEditorPage : ContentPage
{
	public NoteEditorPage(NoteEditorViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}