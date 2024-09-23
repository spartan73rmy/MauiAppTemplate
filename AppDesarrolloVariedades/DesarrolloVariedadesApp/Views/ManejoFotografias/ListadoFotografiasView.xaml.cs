using CommunityToolkit.Maui.Core.Extensions;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.ViewModels.ManejoFotografias;

namespace DesarrolloVariedadesApp.Views.ManejoFotografias;

public partial class ListadoFotografiasView : ContentPage, IQueryAttributable
{
	public ListadoFotografiasView(ListadoFotografiasViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Desarrollo"))
        {
            var viemodel = (ListadoFotografiasViewModel)BindingContext;
            viemodel.plantaDesarrollo = (HistDesarrolloVariedadesApp)query["Desarrollo"];
            viemodel.LoadData();
        }

        //if (query.ContainsKey("Lista"))
        //{
        //    var viemodel = (ListadoFotografiasViewModel)BindingContext;
        //    viemodel.listaImagenes = new List<DesVarImagenesApp>((List<DesVarImagenesApp>)query["Lista"]);
        //    viemodel.ImagenesList = viemodel.listaImagenes.ToObservableCollection();
        //    viemodel.ModifyData();
        //}
    }
}