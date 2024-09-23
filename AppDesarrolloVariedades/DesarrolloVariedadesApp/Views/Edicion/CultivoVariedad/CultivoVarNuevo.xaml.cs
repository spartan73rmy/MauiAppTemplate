using DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo;

namespace DesarrolloVariedadesApp.Views.Edicion.CultivoVariedad;

public partial class CultivoVarNuevo : ContentView
{
	public CultivoVarNuevo(CultivoVariedadNuevoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}