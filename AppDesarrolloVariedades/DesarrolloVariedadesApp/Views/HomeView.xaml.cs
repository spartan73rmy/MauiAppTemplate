using DesarrolloVariedadesApp.ViewModels;

namespace DesarrolloVariedadesApp.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        var viewModel = (HomeViewModel)BindingContext;
		viewModel.CalcularCambios();
		//base.OnAppearing();
    }
}