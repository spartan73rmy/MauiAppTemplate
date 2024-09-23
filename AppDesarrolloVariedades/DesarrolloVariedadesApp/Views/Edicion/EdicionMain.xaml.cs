using DesarrolloVariedadesApp.ViewModels;

namespace DesarrolloVariedadesApp.Views.Edicion;

public partial class EdicionMain : ContentPage
{
	public EdicionMain(EdicionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}