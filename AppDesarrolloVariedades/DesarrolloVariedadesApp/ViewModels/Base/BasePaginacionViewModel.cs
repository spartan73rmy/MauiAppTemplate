using CommunityToolkit.Mvvm.ComponentModel;
using DesarrolloVariedadesApp.Services.Navigation;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.Base
{
    public abstract partial class BasePaginacionViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool prevPageActive = false;

        [ObservableProperty]
        private bool nextPageActive = false;

        [ObservableProperty]
        private string actualPageText;

        public int numberPages;

        public int actualPage = 0;

        public int itemCount = 10;

        public bool inicio = true;

        protected BasePaginacionViewModel(INavigationService navigationService) : base(navigationService)
        {}

        public ObservableCollection<T> RealizarPaginado<T>(List<T> lista)
        {
            var listaRetornada = new ObservableCollection<T>();

            if(numberPages > 0)
                ActualPageText = $"Pagina {actualPage} - {numberPages}";
            else
                ActualPageText = "Pagina 0 - 0";

            int start = (actualPage - 1) * itemCount;
            int end = start + itemCount;

            if (actualPage > 1) PrevPageActive = true;
            else PrevPageActive = false;

            if (end >= lista.Count)
            {
                end = lista.Count;
                NextPageActive = false;
            }
            else NextPageActive = true;

            for (int i = start; i < end; i++)
                listaRetornada.Add(lista[i]);
            return listaRetornada;
        }

    }
}