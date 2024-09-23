using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels;
using DesarrolloVariedadesApp.Views;

namespace DesarrolloVariedadesApp
{
    public partial class App : Application
    {
        private readonly INavigationService navigationService;
        private readonly ICurrentUserAppService user;
        private readonly LoginViewModel loginViewModel;

        public App(INavigationService navigationService, ICurrentUserAppService user, IDataBaseService dataBaseService, LoginViewModel loginViewModel)
        {
            InitializeComponent();
            this.navigationService = navigationService;
            this.user = user;
            this.loginViewModel = loginViewModel;          
            if (user.IsAutenticated)
            {
                var prueba = user.Roles;
                MainPage = new AppShell(this.navigationService, this.user);
                //MainPage = mainPage;
            }
            else
            {
                MainPage = new LoginView(this.loginViewModel);
            }

        }

        public void Logout()
        {
            user.DbApiToken = "";
            // Cierra sesión y navega a la página de inicio de sesión
            MainPage = new LoginView(loginViewModel);
        }

        public void Login()
        {
            // Inicia sesion y navega al appShell
            MainPage = new AppShell(this.navigationService, this.user);
        }
    }
}
