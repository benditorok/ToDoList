namespace ToDoList.Client.Views.Identity;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}