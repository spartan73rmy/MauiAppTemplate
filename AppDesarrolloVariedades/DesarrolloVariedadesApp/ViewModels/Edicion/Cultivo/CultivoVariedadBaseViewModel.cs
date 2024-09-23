using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesarrolloVariedadesApp.Interface;
using DesarrolloVariedadesApp.Messages;
using DesarrolloVariedadesApp.Security;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using DesarrolloVariedadesApp.Views.Edicion.CultivoVariedad;

namespace DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo
{
    public partial class CultivoVariedadBaseViewModel : BaseViewModel
    {
        private readonly ICurrentUserAppService user;
        private CultivoVarListado cultivoVarListado;
        private CultivoVarNuevo cultivoVarNuevo;
        private CultivoVarModificar cultivoVarModificar;

        [ObservableProperty]
        private string titleText;

        [ObservableProperty]
        private ContentView contenidoContenedor;       

        public CultivoVariedadBaseViewModel(INavigationService navigationService, CultivoVarListado cultivoVarListado, CultivoVarNuevo cultivoVarNuevo, CultivoVarModificar cultivoVarModificar, ICurrentUserAppService user) : base(navigationService)
        {
            this.cultivoVarListado = cultivoVarListado;
            this.cultivoVarNuevo = cultivoVarNuevo;
            this.cultivoVarModificar = cultivoVarModificar;
            this.user = user;
            if (!WeakReferenceMessenger.Default.IsRegistered<ModificarVariedadMessage>(this))
                WeakReferenceMessenger.Default.Register<ModificarVariedadMessage>(this, (o, s) =>
                {
                    if (InternetAccess())
                        CambioModificar();
                    else Application.Current?.MainPage?.DisplayAlert("Error", "No cuentas con accesso a Internet para acceder aqui", "Aceptar");
                });
            if (!WeakReferenceMessenger.Default.IsRegistered<ReturnListadoVariedadesMessage>(this))
                WeakReferenceMessenger.Default.Register<ReturnListadoVariedadesMessage>(this, (o, s) =>
                {
                    CambioListado();
                });            
            CambioListado();
        }

        [RelayCommand]
        public void CambioListado()
        {
            TitleText = "Listado - Variedades";
            ContenidoContenedor = cultivoVarListado;
        }

        [RelayCommand]
        public void CambioNuevo()
        {
            if (!user.Roles.Contains(PermisosDesarrolloVariedad.CatalogoNuevo))
            {
                Application.Current?.MainPage?.DisplayAlert("Advertencia", "No cuantas con el permiso para entrar a este modulo.", "Entendido");
                return;
            }
            if (InternetAccess())
            {
                TitleText = "Nuevo - Variedad";
                ContenidoContenedor = cultivoVarNuevo;
            }
            else Application.Current?.MainPage?.DisplayAlert("Error", "No cuentas con accesso a Internet para acceder aqui", "Aceptar");
        }

        private void CambioModificar()
        {
            if (!user.Roles.Contains(PermisosDesarrolloVariedad.CatalogoUpdate))
            {
                Application.Current?.MainPage?.DisplayAlert("Advertencia", "No cuantas con el permiso para entrar a este modulo.", "Entendido");
                return;
            }
            if (InternetAccess())
            {
                TitleText = "Modificar - Variedad";
                ContenidoContenedor = cultivoVarModificar;
            }
            else Application.Current?.MainPage?.DisplayAlert("Error", "No cuentas con accesso a Internet para acceder aqui", "Aceptar");
        }

        partial void OnContenidoContenedorChanged(ContentView value)
        {
            if (value.BindingContext is IBaseViewModel)
            {
                var viewmodel = (IBaseViewModel)value.BindingContext;
                viewmodel.LoadData();
            }
        }
    }
}