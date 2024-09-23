using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.Helpers;
using DesarrolloVariedadesApp.Services.ApiDatabase;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Files;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;

namespace DesarrolloVariedadesApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigationService navigation, IApiDatabaseService apiDatabaseService, ICurrentUserAppService user, IDataBaseService dataBaseService, IFilesServices filesServices) : base(navigation)
        {
            this.apiDatabaseService = apiDatabaseService;
            this.user = user;
            this.dataBaseService = dataBaseService;
            this.filesServices = filesServices;
            _ = CheckAppVersion();
        }
        private readonly IApiDatabaseService apiDatabaseService;
        private readonly ICurrentUserAppService user;
        private readonly IDataBaseService dataBaseService;
        private readonly IFilesServices filesServices;

        private bool needUpdateApp = false;
        private string updateLink = "";
        [ObservableProperty]
        private string usuario = "";

        [ObservableProperty]
        private string password = "";

        private async Task CheckAppVersion()
        {
            await RequestHandler.TaskRequestHandlerBasic(
                async () =>
                {
                    var version = await this.apiDatabaseService.GetVersionApp();
                    if (version.VersionNumber > AppSettings.VersionNumber)
                    {
                        updateLink = version.UpdateLink;
                        bool r;
                        var mensaje = $"Hay una nueva version de la aplicacion.\nVersion actual instalada: {AppSettings.AppVersion}\nVersion nueva: {version.AppVersion}";
                        if (!version.Forzar)
                        {
                            r = await Application.Current.MainPage.DisplayAlert("Nueva Actualizacion", mensaje, "Actualizar", "Mas tarde");
                            if (r)
                            {
                                _ = Launcher.OpenAsync(version.UpdateLink);
                            }
                        }
                        else
                        {
                            needUpdateApp = true;
                            await Application.Current.MainPage.DisplayAlert("Nueva Actualizacion", mensaje, "Actualizar");
                            _ = Launcher.OpenAsync(version.UpdateLink);
                        }

                    }
                });
        }

        [RelayCommand]
        private async Task IniciarSesion()
        {
            if (needUpdateApp)
            {
                await Application.Current.MainPage.DisplayAlert("Actualizacion nueva", "Es necesario actualizar la aplicacion", "Actualizar");
                _ = Launcher.OpenAsync(updateLink);
                return;
            }
            if (Usuario == "" || Password == "")
            {
                Application.Current?.MainPage?.DisplayAlert("Datos Incorrectos", "Por favor ingresa un Usuario y una Contraseña", "Aceptar");
                return;
            }

            await RequestHandler.TaskRequestHandlerBasic(
                async () =>
                {
                    string token = await this.apiDatabaseService.IniciarSesion(Usuario, Password);
                    this.user.DbApiToken = token;
                });

            if (!this.user.IsAutenticated)
            {
                Application.Current?.MainPage?.DisplayAlert("Error 500", "No se pudo validar el token", "Aceptar");
                return;
            }
            var prueba = this.user.Roles;
            if (InternetAccess())
            {
                Loading = true;
                await RequestHandler.TaskRequestHandlerBasic(dataBaseService.SubirData);
                await RequestHandler.TaskRequestHandlerBasic(dataBaseService.ActualizarBD);
                Loading = false;
            }
            else
                await Application.Current.MainPage.DisplayAlert("Aviso", "No cuentas con accesso a Internet para recargar datos", "Entendido");
            Usuario = "";
            Password = "";
            Navigation.Login();

        }
    }
}
