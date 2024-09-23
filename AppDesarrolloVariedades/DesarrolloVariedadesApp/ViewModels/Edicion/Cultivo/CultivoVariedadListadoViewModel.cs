using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Helpers;
using DesarrolloVariedadesApp.Interface;
using DesarrolloVariedadesApp.Messages;
using DesarrolloVariedadesApp.Security;
using DesarrolloVariedadesApp.Services.ApiDatabase;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using Season.DesarrolloBVF.Domain.Responses;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo
{
    public partial class CultivoVariedadListadoViewModel : BasePaginacionViewModel, IBaseViewModel
    {
        private readonly IDataBaseService dataBaseService;

        private readonly IApiDatabaseService apiDatabaseService;
        
        private readonly ICurrentUserAppService user;

        private ObservableCollection<CatProductosApp> filtroVariedades;

        private List<CatProductosApp> listaFiltrada;

        [ObservableProperty]
        private ObservableCollection<CatTiposProdApp> cultivoList = new();

        [ObservableProperty]
        private CatTiposProdApp selectedCultivo;

        [ObservableProperty]
        private bool cultivoActivo;

        [ObservableProperty]
        private ObservableCollection<CatProductosApp> variedadList = new();

        [ObservableProperty]
        private CatProductosApp selectedVariedad;

        [ObservableProperty]
        private ObservableCollection<CatProductosApp> listaVariedades;        

        public CultivoVariedadListadoViewModel(INavigationService navigationService, IDataBaseService dataBaseService, IApiDatabaseService apiDatabaseService, ICurrentUserAppService user) : base(navigationService)
        {
            this.dataBaseService = dataBaseService;
            this.apiDatabaseService = apiDatabaseService;
            this.user = user;

            ListaVariedades = new();
            CultivoList = new() { new CatTiposProdApp() { Descripcion = "Todos", Tipo = 0 } };
            VariedadList = new() { new CatProductosApp() { Producto = 0, Descripcion = "Todos", Tipo = 0 } };
        }

        public async void LoadData()
        {
            TerminoLoad = false;
            Loading = true;

            if (InternetAccess())
                await CaragServer();
            else
                await CaragLocal();

            SelectedCultivo = CultivoList.First();
            await Task.Delay(100);
            FiltrarLista();

            Loading = false;
            TerminoLoad = true;
        }

        partial void OnSelectedCultivoChanged(CatTiposProdApp value)
        {
            if (value != null)
            {
                while (VariedadList.Count > 1)
                    VariedadList.RemoveAt(1);
                var list = VariedadList.ToList();
                if (value.Tipo == 0)
                    CultivoActivo = false;
                else
                {
                    list.AddRange(value.Variedades.ToList());
                    CultivoActivo = true;
                }
                VariedadList = list.ToObservableCollection();
                SelectedVariedad = VariedadList.First();
            }
        }

        partial void OnSelectedVariedadChanged(CatProductosApp value)
        {
            if (value != null)
                FiltrarLista();
        }

        private void FiltrarLista()
        {
            listaFiltrada = filtroVariedades.ToList();
            if (SelectedVariedad != null && SelectedVariedad.Producto != 0)
                listaFiltrada = listaFiltrada.Where(x => x.Producto == SelectedVariedad.Producto && x.Cultivo.Tipo == SelectedCultivo.Tipo).ToList();
            else if (SelectedCultivo != null && SelectedCultivo.Tipo != 0)
                listaFiltrada = listaFiltrada.Where(x => x.Cultivo.Tipo == SelectedCultivo.Tipo).ToList();

            actualPage = 0;
            itemCount = 10;
            if (listaFiltrada.Count % 10 == 0) numberPages = (listaFiltrada.Count / 10);
            else numberPages = (listaFiltrada.Count / 10) + 1;
            NextPage();
        }

        private async Task CaragLocal()
        {
            var db = dataBaseService.Get();
            ListaVariedades = new ObservableCollection<CatProductosApp>();

            filtroVariedades = db.CatVariedades
                .Include(x => x.Cultivo)
                .ToObservableCollection();

            foreach (var cultivo in await db.CatCultivos.Include(x => x.Variedades).ToListAsync())
                CultivoList.Add(cultivo);
        }

        private async Task CaragServer()
        {
            var respuesta = await RequestHandler.TaskRequestHandlerBasic(async () =>
            {
                var response = await this.apiDatabaseService.GetRequest<GetCatCultivosQueryResponse>("CatCultivos/GetCultivos");
                var cultivos = response.Cultivos.Select(x => new CatTiposProdApp(x));
                var variedades = response.Variedades.Select(x => new CatProductosApp(x));

                foreach (var cultivo in cultivos)
                {
                    cultivo.Variedades = variedades.Where(j => j.Tipo == cultivo.Tipo).ToList();
                    CultivoList.Add(cultivo);
                }
                filtroVariedades = variedades.Select(x => { x.Cultivo = cultivos.Where(j => j.Tipo == x.Tipo).First(); return x; }).ToObservableCollection();
            });

            if (!respuesta)
                await CaragLocal();
        }

        [RelayCommand]
        public void ModificarDato(object obj)
        {
            if (obj is ImageButton button)
                if (user.Roles.Contains(PermisosDesarrolloVariedad.CatalogoUpdate))
                {
                    if (button.BindingContext is CatProductosApp var)
                        WeakReferenceMessenger.Default.Send(new ModificarVariedadMessage(var));
                }
                else Application.Current?.MainPage?.DisplayAlert("Advertencia", "No cuentas con accesso a este modulo.", "Entendido");
        }

        [RelayCommand]
        public void NextPage()
        {
            actualPage += 1;
            ListaVariedades.Clear();
            ListaVariedades = RealizarPaginado(listaFiltrada);
        }

        [RelayCommand]
        public void PrevPage()
        {
            actualPage -= 1;
            ListaVariedades.Clear();
            ListaVariedades = RealizarPaginado(listaFiltrada);
        }
    }
}