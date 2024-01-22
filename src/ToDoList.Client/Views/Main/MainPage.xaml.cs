namespace ToDoList.Client.Views.Main;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}