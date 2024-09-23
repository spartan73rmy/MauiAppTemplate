using DesarrolloVariedadesApp.Security;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.Views;
using DesarrolloVariedadesApp.Views.Common;
using DesarrolloVariedadesApp.Views.Configuracion;
using DesarrolloVariedadesApp.Views.Configuracion.Impresoras;
using DesarrolloVariedadesApp.Views.Edicion.CultivoVariedad;
using DesarrolloVariedadesApp.Views.ManejoFotografias;

namespace DesarrolloVariedadesApp
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService navigationService;
        
        public AppShell(INavigationService navigationService, ICurrentUserAppService user)
        {
            this.navigationService = navigationService;

            InitializeComponent();
            ConfigureRoutes();

            catalogos.IsVisible = user.Roles.Contains(PermisosDesarrolloVariedad.Catalogos);

            configuracion.IsVisible = user.Roles.Contains(PermisosDesarrolloVariedad.Configuracion);
        }

        private void LogOutCommand(object sender, EventArgs e) => this.navigationService.logOut();

        public void ConfigureRoutes()
        {
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));

            Routing.RegisterRoute(nameof(ListadoFotografiasView), typeof(ListadoFotografiasView));
            Routing.RegisterRoute(nameof(AddPhotosView), typeof(AddPhotosView));
            Routing.RegisterRoute(nameof(HistorialDesarrolloView), typeof(HistorialDesarrolloView));

            Routing.RegisterRoute(nameof(DisableOrigenView), typeof(DisableOrigenView));

            Routing.RegisterRoute(nameof(CultivoVarBase), typeof(CultivoVarBase));

            Routing.RegisterRoute(nameof(CameraCode), typeof(CameraCode));

            Routing.RegisterRoute(nameof(ConfiguracionMain), typeof(ConfiguracionMain));
            Routing.RegisterRoute(nameof(ImpresorasConfigView), typeof(ImpresorasConfigView));
        }
    }
}