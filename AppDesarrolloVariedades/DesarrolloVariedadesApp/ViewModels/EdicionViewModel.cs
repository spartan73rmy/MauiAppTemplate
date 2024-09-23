using DesarrolloVariedadesApp.Security;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using DesarrolloVariedadesApp.Views.Edicion.CultivoVariedad;
using System.Windows.Input;

namespace DesarrolloVariedadesApp.ViewModels
{
    public class EdicionViewModel : BaseViewModel
    {
        public ICommand ChangeCultivos { get; set; }

        public EdicionViewModel(INavigationService navigationService) : base(navigationService)
        {
            ChangeCultivos = new Command(async () => await Navigation.NavigateToAsyncPermiso(nameof(CultivoVarBase), PermisosDesarrolloVariedad.Catalogos));
        }
    }
}