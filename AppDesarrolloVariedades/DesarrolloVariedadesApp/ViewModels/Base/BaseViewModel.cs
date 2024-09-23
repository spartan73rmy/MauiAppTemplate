using CommunityToolkit.Mvvm.ComponentModel;
using DesarrolloVariedadesApp.Services.Navigation;

namespace DesarrolloVariedadesApp.ViewModels.Base
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool terminoLoad;

        [ObservableProperty]
        private bool loading;

        public IReadOnlyList<Page> ModalStack => throw new NotImplementedException();

        public IReadOnlyList<Page> NavigationStack => throw new NotImplementedException();

        public INavigationService Navigation { get; set; }

        public BaseViewModel(INavigationService navigationService)
            => Navigation = navigationService;

       

        public bool InternetAccess()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
                return true;
            return false;
        }
    }
}