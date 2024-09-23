using DesarrolloVariedadesApp.ViewModels;

namespace DesarrolloVariedadesApp.Views;

public partial class LoginView : ContentPage
{
	public LoginView( LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}