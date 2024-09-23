using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.ViewModels.ManejoFotografias;

namespace DesarrolloVariedadesApp.Views.ManejoFotografias;

public partial class HistorialDesarrolloView : ContentPage, IQueryAttributable
{
	public HistorialDesarrolloView(HistorialDesarrolloViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var viemodel = (HistorialDesarrolloViewModel)BindingContext;

        if (query.ContainsKey("Desarrollo"))
        {
            viemodel.desarrolloPlanta = (HistDesarrolloVariedadesApp)query["Desarrollo"];
            viemodel.LoadData();
        }
    }
}