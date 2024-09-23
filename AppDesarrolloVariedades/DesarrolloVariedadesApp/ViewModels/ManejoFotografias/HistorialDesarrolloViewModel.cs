using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using DesarrolloVariedadesApp.Views.ManejoFotografias;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.ManejoFotografias
{
    public partial class HistorialDesarrolloViewModel : BasePaginacionViewModel
    {
        private readonly IDataBaseService dataBaseService;

        [ObservableProperty]
        private ObservableCollection<DesVarMovimientosApp> listaImagenes = new();

        [ObservableProperty]
        private string titulo;

        private List<DesVarMovimientosApp> listaFiltrada;

        public HistDesarrolloVariedadesApp desarrolloPlanta;

        public HistorialDesarrolloViewModel(INavigationService navigationService, IDataBaseService dataBaseService) : base(navigationService)
        {
            this.dataBaseService = dataBaseService;
        }

        public void LoadData()
        {
            Titulo = $"Historial de Desarrollo: {desarrolloPlanta.IdDesarrollo}";
            var db = dataBaseService.Get();
            listaFiltrada = db.DesVarMovimientos.Where(x => x.IdDesarrollo == desarrolloPlanta.IdDesarrollo).ToList();
            var mov = db.DesVarMovimientosTipos.ToList();

            foreach(var img in  listaFiltrada)
            {
                var res = mov.Where(x => x.IdMovimientoTipo == img.IdTipo).SingleOrDefault();
                img.TipoAccion = res.Descripcion;
            }

            actualPage = 0;
            itemCount = 10;
            if (listaFiltrada.Count % 10 == 0) numberPages = (listaFiltrada.Count / 10);
            else numberPages = (listaFiltrada.Count / 10) + 1;
            NextPage();
        }

        [RelayCommand]
        public void NextPage()
        {
            actualPage += 1;
            ListaImagenes.Clear();
            ListaImagenes = RealizarPaginado(listaFiltrada);
        }

        [RelayCommand]
        public void PrevPage()
        {
            actualPage -= 1;
            ListaImagenes.Clear();
            ListaImagenes = RealizarPaginado(listaFiltrada);
        }

        [RelayCommand]
        public void ImagenClicked(object obj)
        {
            if (obj is Image imagen)
                if (imagen.BindingContext is DesVarMovimientosApp img)
                {
                    var currentPage = Application.Current.MainPage;
                    if (currentPage is Page page)
                        page.ShowPopup(new PopupImagenZoom(new PopupImagenZoomViewModel(Navigation, null, img)));
                }
        }
    }
}