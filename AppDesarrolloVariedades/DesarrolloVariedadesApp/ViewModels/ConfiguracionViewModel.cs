using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using DesarrolloVariedadesApp.Views.Configuracion.Impresoras;
using System.Windows.Input;

namespace DesarrolloVariedadesApp.ViewModels
{
    public class ConfiguracionViewModel : BaseViewModel
    {

        public ConfiguracionViewModel(INavigationService navigationService) : base(navigationService)
        {
            ImpresoraCommand = new Command(async () => await Navigation.NavigateToAsync(nameof(ImpresorasConfigView)));
        }

        public ICommand ImpresoraCommand { get; set; }


    }
}
