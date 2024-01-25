namespace ToDoList.Client.Views.NoteLists;

public partial class NoteSelectionPage : ContentPage
{
	public NoteSelectionPage(NoteSelectionViewModel viewModel)
	{
		BindingContext = viewModel;
		Loaded += viewModel.OnPageLoaded;
		InitializeComponent();
	}
}