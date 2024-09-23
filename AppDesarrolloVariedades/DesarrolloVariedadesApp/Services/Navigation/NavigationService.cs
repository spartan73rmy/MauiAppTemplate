using CommunityToolkit.Maui.Views;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Views;

namespace DesarrolloVariedadesApp.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly ICurrentUserAppService user;

        public NavigationService(ICurrentUserAppService user)
        {
            this.user = user;
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync(nameof(LoginView));
        }

        public void Login()
        {
            ((App)Application.Current).Login();
        }

        public void logOut()
        {
            ((App)Application.Current).Logout();
        }

        public Task NavigateToAsync(string ruta, IDictionary<string, object> rutaParametros = null)
        {
            
            var shellNavigation = new ShellNavigationState(ruta);
            return rutaParametros != null
                ? Shell.Current.GoToAsync(shellNavigation, rutaParametros)
                : Shell.Current.GoToAsync(shellNavigation);
        }

        public Task NavigateToAsyncPermiso(string ruta, string permiso, IDictionary<string, object> rutaParametros = null)
        {
            if(!user.Roles.Contains(permiso))
                return Application.Current.MainPage.DisplayAlert("Advertencia", "No cuantas con el permiso para entrar a este modulo.", "Entendido");

            var shellNavigation = new ShellNavigationState(ruta);
            return rutaParametros != null
                ? Shell.Current.GoToAsync(shellNavigation, rutaParametros)
                : Shell.Current.GoToAsync(shellNavigation);
        }


        public Task NavigateToPrincipalAsync(string menu, string ruta, IDictionary<string, object> rutaParametros = null)
        {
            Shell.Current.CurrentItem = Shell.Current.Items.First(x => x.Title == menu);
            var shellNavigation = new ShellNavigationState(ruta);
            return rutaParametros != null
                ? Shell.Current.GoToAsync(shellNavigation, rutaParametros)
                : Shell.Current.GoToAsync(shellNavigation);
        }

        public Task PopAsync() =>
            Shell.Current.GoToAsync("..");

        public Task ShowPopUpAsync(Popup popup)
        {
            return Shell.Current.ShowPopupAsync(popup);
        }
    }
}