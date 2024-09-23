using CommunityToolkit.Maui.Core.Extensions;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.ViewModels.ManejoFotografias;

namespace DesarrolloVariedadesApp.Views.ManejoFotografias;

public partial class AddPhotosView : ContentPage, IQueryAttributable
{
	public AddPhotosView(AddPhotosViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var viemodel = (AddPhotosViewModel)BindingContext;

        if (query.ContainsKey("Tipo"))
            viemodel.Tipo = (TiposConsula)query["Tipo"];

        if (query.ContainsKey("Lista"))
        {
            var lista = new List<DesVarImagenesApp>((List<DesVarImagenesApp>)query["Lista"]);
            viemodel.ImagenesList = lista.ToObservableCollection();
            viemodel.isListaTemporal = true;
        }

        if (query.ContainsKey("Comentarios"))
        {
            viemodel.HayComentarios = (bool)query["Comentarios"];
        }

        if (query.ContainsKey("Desarrollo"))
            viemodel.desarrolloPlanta = (HistDesarrolloVariedadesApp)query["Desarrollo"];
    }
}