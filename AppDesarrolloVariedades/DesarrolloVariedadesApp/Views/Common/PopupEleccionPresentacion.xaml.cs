using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Common;

namespace DesarrolloVariedadesApp.Views.Common;

public partial class PopupEleccionPresentacion : Popup
{ 
	public PopupEleccionPresentacion(INavigationService navigationService, IDataBaseService dataBaseService, TiposConsula tiposConsula)
	{
		InitializeComponent();
		BindingContext = new PopupEleccionPresentacionViewModel(this, navigationService, dataBaseService) { Tipo = tiposConsula};
	}
}