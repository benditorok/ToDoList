namespace ToDoList.Client.Views.NoteLists;

public partial class NoteListPage : ContentPage
{
	public NoteListPage(NoteListViewModel viewModel)
	{
		BindingContext = viewModel;
		Loaded += viewModel.OnPageLoaded;
		InitializeComponent();
	}
}