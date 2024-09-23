using CommunityToolkit.Mvvm.ComponentModel;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;

namespace DesarrolloVariedadesApp.ViewModels.ManejoFotografias
{
    public partial class PopupImagenZoomViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string url, comentario;

        [ObservableProperty]
        private bool hayComentarios;

        [ObservableProperty]
        private int height;

        public PopupImagenZoomViewModel(INavigationService navigationService, DesVarImagenesApp imagen, DesVarMovimientosApp motivo = null, bool hayComentarios = true) : base(navigationService)
        {
            if (imagen != null)
            {
                Url = imagen.FullPath;
                Comentario = imagen.Comentario;
            }
            else
            {
                Url = motivo.FullPath;
                Comentario = motivo.Comentario;
            }
            if (hayComentarios)
                Height = 500;
            else
                Height = 350;
            HayComentarios = hayComentarios;
        }
    }
}