using DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo;

namespace DesarrolloVariedadesApp.Views.Edicion.CultivoVariedad;

public partial class CultivoVarModificar : ContentView
{
	public CultivoVarModificar(CultivoVariedadModificarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}