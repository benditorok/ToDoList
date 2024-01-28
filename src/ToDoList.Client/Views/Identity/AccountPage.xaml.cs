namespace ToDoList.Client.Views.Identity;

public partial class AccountPage : ContentPage
{
	public AccountPage(AccountViewModel viewModel)
	{
		BindingContext = viewModel;
		Loaded += viewModel.OnPageLoaded;
		InitializeComponent();
	}
}