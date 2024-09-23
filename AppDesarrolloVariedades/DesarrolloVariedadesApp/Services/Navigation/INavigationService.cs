using CommunityToolkit.Maui.Views;

namespace DesarrolloVariedadesApp.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync(string ruta, IDictionary<string, object> rutaParametros = null);
        Task NavigateToPrincipalAsync(string menu, string ruta, IDictionary<string, object> rutaParametros = null);

        Task NavigateToAsyncPermiso(string ruta, string permiso, IDictionary<string, object> rutaParametros = null);

        void logOut();

        void Login();
        Task PopAsync();
        Task ShowPopUpAsync(Popup popup);
    }
}