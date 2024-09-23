using DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo;

namespace DesarrolloVariedadesApp.Views.Edicion.CultivoVariedad;

public partial class CultivoVarBase : ContentPage
{
	public CultivoVarBase(CultivoVariedadBaseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}