namespace ToDoList.Client.Views.Identity;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}