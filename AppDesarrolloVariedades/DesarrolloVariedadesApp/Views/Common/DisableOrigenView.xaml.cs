using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.ViewModels.Common;

namespace DesarrolloVariedadesApp.Views.Common;

public partial class DisableOrigenView : ContentPage, IQueryAttributable
{
	public DisableOrigenView(DisableOrigenViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Planta"))
        {
            var viemodel = (DisableOrigenViewModel)BindingContext;
            viemodel.plantaDesarrollo = (HistDesarrolloVariedadesApp)query["Planta"];
        }
    }
}