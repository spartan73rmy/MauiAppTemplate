using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Helpers;
using DesarrolloVariedadesApp.Interface;
using DesarrolloVariedadesApp.Services.ApiDatabase;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using Season.DesarrolloBVF.Domain.Responses;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo
{
    public partial class CultivoVariedadNuevoViewModel : BaseViewModel, IBaseViewModel
    {
        private readonly IDataBaseService dataBaseService;

        private readonly IApiDatabaseService apiDatabaseService;

        [ObservableProperty]
        private ObservableCollection<CatTiposProdApp> cultivoList = new();

        [ObservableProperty]
        private CatTiposProdApp selectedCultivo;

        [ObservableProperty]
        private bool isValidDescripcion, isValidAbreviatura, isValidCultivo, visibleVariedad, visibleCultivo, isOrganica;

        [ObservableProperty]
        private string descripcionText, abreviaturaText, cultivoText, nombreInternoText;

        public CultivoVariedadNuevoViewModel(INavigationService navigationService, IDataBaseService dataBaseService, IApiDatabaseService apiDatabaseService) : base(navigationService)
        {
            this.dataBaseService = dataBaseService;
            this.apiDatabaseService = apiDatabaseService;
        }

        public async void LoadData()
        {
            await RequestHandler.TaskRequestHandlerBasic(async () =>
            {
                VisibleVariedad = true;
                var response = await apiDatabaseService.GetRequest<GetCatCultivosQueryResponse>("CatCultivos/GetCultivos");

                var cultivos = response.Cultivos.Select(x => new CatTiposProdApp(x));

                CultivoList.Clear();
                foreach (var cultivo in cultivos)
                    CultivoList.Add(cultivo);
            });
        }

        private void LimpiarCultivo()
        {
            CultivoText = null;
            ReturnVariedad();
        }

        [RelayCommand]
        public async Task AgregarVariedad()
        {
            if (SelectedCultivo == null || !IsValidDescripcion || !IsValidAbreviatura)
            {
                Application.Current?.MainPage?.DisplayAlert("Error 401", "Necesitas llenar correctamente los datos", "Entendido");
                return;
            }

            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea agregar este nuevo registro?", "Aceptar", "Cancelar");
            if (!result)
                return;

            await RequestHandler.TaskRequestHandlerBasic(async () =>
            {
                var response = await apiDatabaseService.GetRequest<GetCatCultivosQueryResponse>("CatCultivos/GetCultivos");

                var variedades = response.Variedades.Where(x => x.Descripcion.ToUpper().Equals(DescripcionText)
                                                        && x.Tipo == SelectedCultivo.Tipo
                                                        && x.Abreviacion.ToUpper().Equals(AbreviaturaText)
                                                        && x.Organico == IsOrganica).ToList();

                if (variedades.Count > 0)
                {
                    Application.Current?.MainPage?.DisplayAlert("Error 401", "La variedad ingresado ya existe.", "Entendido");
                    return;
                }

                var variedad = new CatProductosApp()
                {
                    NombreInterno = NombreInternoText,
                    Abreviacion = AbreviaturaText,
                    Descripcion = DescripcionText,
                    Tipo = SelectedCultivo.Tipo,
                    Organico = IsOrganica,
                    DescripcionIngles = NombreInternoText
                };

                var res = dataBaseService.SetVariedadJSON(variedad, 'N');

                if (res != null)
                    await apiDatabaseService.PostRequest("CatCultivos/AddVariedad", res);

                LimpiarData();
                LoadData();
                await dataBaseService.ActualizarCatalogos();
                Application.Current?.MainPage?.DisplayAlert("Registro Exitoso", "Su registro fue añadido exitosamente", "Entendido");
            });
        }

        [RelayCommand]
        public async Task AgregarCultivo()
        {
            if (!IsValidCultivo)
            {
                Application.Current?.MainPage?.DisplayAlert("Error 401", "Necesitas llenar correctamente los datos", "Entendido");
                return;
            }

            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea agregar este nuevo registro?", "Aceptar", "Cancelar");
            if (!result)
                return;

            await RequestHandler.TaskRequestHandlerBasic(async () =>
            {
                var response = await apiDatabaseService.GetRequest<GetCatCultivosQueryResponse>("CatCultivos/GetCultivos");

                var cultivos = response.Cultivos.Where(x => x.Descripcion.ToUpper().Equals(CultivoText)).ToList();
                if (cultivos.Count > 0)
                {
                    Application.Current?.MainPage?.DisplayAlert("Error 401", "EL cultivo ingresado ya existe.", "Entendido");
                    return;
                }

                var cultivo = new CatTiposProdApp()
                {
                    Descripcion = CultivoText
                };

                var res = dataBaseService.SetCultivoJSON(cultivo);

                if (res != null)
                    await apiDatabaseService.PostRequest("CatCultivos/AddCultivo", res);

                LimpiarCultivo();
                LoadData();
                await dataBaseService.ActualizarCatalogos();
                Application.Current?.MainPage?.DisplayAlert("Registro Exitoso", "Su registro fue añadido exitosamente", "Entendido");
            });
        }

        [RelayCommand]
        public void EnableCultivo()
        {
            VisibleVariedad = false;
            VisibleCultivo = true;
        }

        [RelayCommand]
        public void LimpiarData()
        {
            SelectedCultivo = null;
            DescripcionText = null;
            AbreviaturaText = null;
            NombreInternoText = null;
            IsOrganica = false;
        }

        [RelayCommand]
        public void ReturnVariedad()
        {
            VisibleVariedad = true;
            VisibleCultivo = false;
        }
    }
}
