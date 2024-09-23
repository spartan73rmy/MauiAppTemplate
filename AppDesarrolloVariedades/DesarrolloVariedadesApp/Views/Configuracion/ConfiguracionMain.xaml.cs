using DesarrolloVariedadesApp.ViewModels;

namespace DesarrolloVariedadesApp.Views.Configuracion;

public partial class ConfiguracionMain : ContentPage
{
	public ConfiguracionMain(ConfiguracionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}