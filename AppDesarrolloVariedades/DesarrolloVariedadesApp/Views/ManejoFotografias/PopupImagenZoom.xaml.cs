using CommunityToolkit.Maui.Views;
using DesarrolloVariedadesApp.ViewModels.ManejoFotografias;

namespace DesarrolloVariedadesApp.Views.ManejoFotografias;

public partial class PopupImagenZoom : Popup
{
	public PopupImagenZoom(PopupImagenZoomViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}