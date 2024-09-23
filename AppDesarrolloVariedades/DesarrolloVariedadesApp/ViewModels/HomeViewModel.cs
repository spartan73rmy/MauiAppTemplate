using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.Helpers;
using DesarrolloVariedadesApp.Messages;
using DesarrolloVariedadesApp.Security;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Files;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using DesarrolloVariedadesApp.Views.Common;
using DesarrolloVariedadesApp.Views.Edicion;
using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;
using System.Windows.Input;

namespace DesarrolloVariedadesApp.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IDataBaseService dataBaseService;
        private readonly IFilesServices filesServices;
        private readonly ICurrentUserAppService user;

        public ICommand ChangeHistDesarrolloCommand { get; set; }
        public ICommand ChangeNodoCommand { get; set; }
        public ICommand ChangeReportesCommand { get; set; }
        public ICommand ChangeCatalogosCommand { get; set; }

        [ObservableProperty]
        private DateTime ultimaActualizacion;

        [ObservableProperty]
        private int pendientes;

        [ObservableProperty]
        private string userName, entryLote;

        [ObservableProperty]
        private bool origenActivo, nodoActivo, reportesActivo, catalogosActivo, cargando = true;

        public HomeViewModel(INavigationService navigationService, IDataBaseService dataBaseService, IFilesServices filesServices, ICurrentUserAppService user) : base(navigationService)
        {

            this.dataBaseService = dataBaseService;
            this.filesServices = filesServices;
            this.user = user;
            if (!WeakReferenceMessenger.Default.IsRegistered<ConsultarTxtMessage>(this))
                WeakReferenceMessenger.Default.Register<ConsultarTxtMessage>(this, async (o, s) =>
                {
                    if (s.ValorBuscado.IsNullOrEmpty())
                    { return; }
                    if (s.Tipo == TiposConsula.General)
                    {
                        _ = BuscarData(s.ValorBuscado);
                    }
                });
            OrigenActivo = user.Roles.Contains(PermisosDesarrolloVariedad.Origen);
            NodoActivo = user.Roles.Contains(PermisosDesarrolloVariedad.Nodo);
            ReportesActivo = user.Roles.Contains(PermisosDesarrolloVariedad.Reportes);
            CatalogosActivo = user.Roles.Contains(PermisosDesarrolloVariedad.Catalogos);
            ChangeCatalogosCommand = new Command(async () => await Navigation.NavigateToAsyncPermiso($"///{nameof(EdicionMain)}", PermisosDesarrolloVariedad.Catalogos));

            UltimaActualizacion = this.dataBaseService.UltimaActualizacion;
        }

        [RelayCommand]
        public async Task EscanearCodigoQR() => await Navigation.NavigateToAsync(nameof(CameraCode), new Dictionary<string, object> { { "Tipo", TiposConsula.General } });
        [RelayCommand]
        public async Task BuscarData(string valor)
        {
            var db = this.dataBaseService.Get();
            try
            {
                var desarrollo = db.HistDesarrolloVariedades.FirstOrDefault(x => x.Lote == valor || x.IdDesarrollo.ToString() == valor);
                if (desarrollo != null)
                {
                    switch (desarrollo.IdTipoDesarrollo)
                    {
                        case (int)TiposDesarrolloVariedad.NODO:
                            throw new NotImplementedException("No se ha programado el comportamiento");
                        case (int)TiposDesarrolloVariedad.Inv:
                            throw new NotImplementedException("No se ha programado el comportamiento");
                        case (int)TiposDesarrolloVariedad.Genetica:
                            throw new NotImplementedException("No se ha programado el comportamiento");
                        default:
                            throw new KeyNotFoundException("Tipo de desarrollo no encontrado");
                    }
                }
            }
            catch
            {
                Application.Current?.MainPage?.DisplayAlert("Error 401", "Existe mas de un registro con el mismo identificador.\nReportelo a soporte.", "Entendido");
            }
        }


        [RelayCommand]
        public async Task ActualizarData()
        {
            if (InternetAccess())
            {
                Cargando = false;
                Loading = true;
                if (dataBaseService.DbStatus == EEstatusBD.ConPendientes)
                {
                    await RequestHandler.TaskRequestHandlerBasic(
                        async () =>
                        {
                            await dataBaseService.ActualizarCatalogos();
                            Application.Current?.MainPage?.DisplayAlert("Sincronizacion Incompleta", "Solo se actualizaron los catalogos ya que tiene registros pendientes por subir", "Entendido");
                        });
                }
                else
                    await RequestHandler.TaskRequestHandlerBasic(dataBaseService.ActualizarBD);

                UltimaActualizacion = this.dataBaseService.UltimaActualizacion;
                CalcularCambios();
                Loading = false;
                Cargando = true;
                Navigation.Login();
            }
            else
                Application.Current?.MainPage?.DisplayAlert("Advertencia", "No cuentas con accesso a Internet para recargar datos", "Entendido");
        }

        [RelayCommand]
        public async Task SubirData()
        {
            if (InternetAccess())
            {
                Loading = true;
                await RequestHandler.TaskRequestHandlerBasic(dataBaseService.SubirData);
                await RequestHandler.TaskRequestHandlerBasic(dataBaseService.ActualizarBD);
                UltimaActualizacion = this.dataBaseService.UltimaActualizacion;
                CalcularCambios();
                Loading = false;
                Navigation.Login();
            }
            else
                await Application.Current.MainPage.DisplayAlert("Advertencia", "No cuentas con accesso a Internet para recargar datos", "Entendido");
        }

        public void CalcularCambios()
        {
            Pendientes = dataBaseService.NuevosRegistros() + dataBaseService.RegistrosActualizados();
            UserName = "Usuario: " + user.UserName;
        }

        [RelayCommand]
        public async Task DescargarJson()
        {
            if (Pendientes <= 0)
            {
                Application.Current?.MainPage?.DisplayAlert("Aviso", "No hay pendientes", "Entendido");
                return;
            }
            var carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DesarrolloVariedades");
            var zip = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pendientes.zip");
            var filePathImagenes = Path.Combine(carpeta, "Imagenes");
            var mensaje = "";
            var jsonA = await dataBaseService.GetJsonDesarrolloUbicaciones("A");
            var jsonM = await dataBaseService.GetJsonDesarrollo("M");
            var jsonD = await dataBaseService.GetJsonDesarrollo("D");
            var jsonUM = await dataBaseService.GetJsonUbicaciones("M");

            var jsonI = await dataBaseService.GetJsonImagenes("A");
            var jsonIM = await dataBaseService.GetJsonImagenes("M");

            var imagenes = dataBaseService.Get().DesVarImagenes.Where(x => x.TipoAccion != null);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
            if (!Directory.Exists(filePathImagenes))
            {
                Directory.CreateDirectory(filePathImagenes);
            }

            await Task.CompletedTask;

            try
            {
                if (jsonA != null)
                {
                    await File.WriteAllTextAsync(Path.Combine(carpeta, "DesarrollosNuevos.json"), jsonA);
                }
                if (jsonM != null)
                {
                    await File.WriteAllTextAsync(Path.Combine(carpeta, "DesarollosModificados.json"), jsonM);
                }
                if (jsonUM != null)
                {
                    await File.WriteAllTextAsync(Path.Combine(carpeta, "DesarrollosNuevos.json"), jsonUM);
                }
                if (jsonI != null)
                {
                    await File.WriteAllTextAsync(Path.Combine(carpeta, "ImagenesNuevas.json"), jsonI);
                }
                if (jsonIM != null)
                {
                    await File.WriteAllTextAsync(Path.Combine(carpeta, "ImagenesActualizadas.json"), jsonIM);
                }
                if (jsonD != null)
                {
                    await File.WriteAllTextAsync(Path.Combine(carpeta, "DesarrollosInactivados.json"), jsonD);
                }
                foreach (var imagen in imagenes)
                {
                    File.Copy(imagen.FullPath, Path.Combine(filePathImagenes, Path.GetFileName(imagen.UrlImagen)), overwrite: true);
                }

                if (File.Exists(zip))
                {
                    File.Delete(zip);
                }

                ZipFile.CreateFromDirectory(carpeta, zip);

                mensaje = $"Archivo creado en: {carpeta}";
            }
            catch (Exception ex)
            {
                mensaje = $"Error al crear el archivo JSON: {ex.Message}";
                return;
            }


#if ANDROID
            var file = new ShareFile(zip);
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Enviar código QR",
                File = file
            });
            await Task.WhenAll();
            mensaje = "Archivo Compartido exitosamente";
            File.Delete(zip);
#endif
            Application.Current?.MainPage?.DisplayAlert("Archivo Compartido", mensaje, "Aceptar");

        }
    }
}
