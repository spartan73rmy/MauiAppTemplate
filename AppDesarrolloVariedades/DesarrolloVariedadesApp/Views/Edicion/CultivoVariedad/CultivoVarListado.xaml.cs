using DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo;

namespace DesarrolloVariedadesApp.Views.Edicion.CultivoVariedad;

public partial class CultivoVarListado : ContentView
{
	public CultivoVarListado(CultivoVariedadListadoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}