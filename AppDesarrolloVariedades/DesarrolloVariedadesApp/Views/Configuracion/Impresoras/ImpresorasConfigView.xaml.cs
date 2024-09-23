using DesarrolloVariedadesApp.ViewModels.Configuracion.Impresoras;

namespace DesarrolloVariedadesApp.Views.Configuracion.Impresoras;

public partial class ImpresorasConfigView : ContentPage
{
	public ImpresorasConfigView(ImpresorasConfigViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}