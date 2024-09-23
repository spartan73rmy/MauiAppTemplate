using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Helpers;
using DesarrolloVariedadesApp.Messages;
using DesarrolloVariedadesApp.Services.ApiDatabase;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using Season.DesarrolloBVF.Domain.Responses;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo
{
    public partial class CultivoVariedadModificarViewModel : BaseViewModel
    {
        private readonly IDataBaseService dataBaseService;

        private readonly IApiDatabaseService apiDatabaseService;

        private ObservableCollection<CatProductosApp> variedadList;

        private CatProductosApp variedadModificado, originalModificado;

        [ObservableProperty]
        private bool isValidAbreviatura, isOrganica;

        [ObservableProperty]
        private string descripcionText, abreviaturaText, inputCultivoText, nombreInterno;      

        public CultivoVariedadModificarViewModel(INavigationService navigationService, IDataBaseService dataBaseService, IApiDatabaseService apiDatabaseService) : base(navigationService)
        {
            this.dataBaseService = dataBaseService;
            this.apiDatabaseService = apiDatabaseService;

            if (!WeakReferenceMessenger.Default.IsRegistered<ModificarVariedadMessage>(this))
                WeakReferenceMessenger.Default.Register<ModificarVariedadMessage>(this, (o, s) =>
                {
                    if (InternetAccess())
                    {
                        if (s.ValorBuscado == null)
                            return;
                        variedadModificado = s.ValorBuscado;
                        originalModificado = s.ValorBuscado;
                        LoadData();
                    }
                });
        }

        private async void LoadData()
        {
            await RequestHandler.TaskRequestHandlerBasic(async () =>
            {
                var response = await apiDatabaseService.GetRequest<GetCatCultivosQueryResponse>("CatCultivos/GetCultivos");

                var variedades = response.Variedades.Select(x => new CatProductosApp(x));

                variedadList = variedades.ToObservableCollection();
                InputCultivoText = response.Cultivos.Where(x => x.Tipo == variedadModificado.Tipo).SingleOrDefault().Descripcion;
                DescripcionText = variedadModificado.Descripcion;
                AbreviaturaText = variedadModificado.Abreviacion;
                IsOrganica = variedadModificado.Organico;
                NombreInterno = variedadModificado.NombreInterno;
            });
        }

        [RelayCommand]
        public async Task ModicarVariedad()
        {
            if (!IsValidAbreviatura)
            {
                Application.Current?.MainPage?.DisplayAlert("Error 401", "Necesitas llenar correctamente los datos", "Entendido");
                return;
            }

            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea realizar modificaciones sobre este registro?", "Aceptar", "Cancelar");
            if (!result)
                return;

            var variedades = variedadList.Where(x => x.Tipo == originalModificado.Tipo);
            foreach (var item in variedades)
            {
                if (item.Producto != originalModificado.Producto)
                {
                    if (item.Descripcion.ToUpper().Equals(DescripcionText) && item.Organico == IsOrganica)
                    {
                        Application.Current?.MainPage?.DisplayAlert("Error 401", "La variedad ingresado ya existe.", "Entendido");
                        return;
                    }
                }
                else if (item.Descripcion.ToUpper().Equals(DescripcionText) && item.Abreviacion.ToUpper().Equals(AbreviaturaText) && item.Organico == IsOrganica)
                {
                    Application.Current?.MainPage?.DisplayAlert("Advertencia", "No hubo datos modificados", "Entendido");
                    WeakReferenceMessenger.Default.Send(new ReturnListadoVariedadesMessage());
                    return;
                }
            }
            variedadModificado.Abreviacion = AbreviaturaText;
            variedadModificado.Organico = IsOrganica;

            await RequestHandler.TaskRequestHandlerBasic(async () =>
            {
                var res = dataBaseService.SetVariedadJSON(variedadModificado, 'M');

                if (res != null)
                    await apiDatabaseService.PostRequest("CatCultivos/UpdateVariedad", res);

                Application.Current?.MainPage?.DisplayAlert("Modificacion Exitosa", "Su registro fue modificado exitosamente", "Entendido");
                await dataBaseService.ActualizarCatalogos();
                WeakReferenceMessenger.Default.Send(new ReturnListadoVariedadesMessage());
            });
        }

        [RelayCommand]
        public async Task ReturnMain()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Quieres dejar de modificar este registro?", "Aceptar", "Cancelar");
            if (!result)
                return;
            WeakReferenceMessenger.Default.Send(new ReturnListadoVariedadesMessage());
        }
    }
}
