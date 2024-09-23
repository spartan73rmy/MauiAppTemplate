using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.Messages;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Files;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.Common
{
    public partial class DisableOrigenViewModel : BaseViewModel
    {
        private readonly IDataBaseService dataBaseService;
        private readonly IFilesServices filesServices;

        [ObservableProperty]
        private string url = "", photoComment = "";

        [ObservableProperty]
        private DesVarMovimientosTiposApp selectedMotivo;

        [ObservableProperty]
        private ObservableCollection<DesVarMovimientosTiposApp> motivosList = new();

        public HistDesarrolloVariedadesApp plantaDesarrollo = new();

        public DisableOrigenViewModel(INavigationService navigationService, IDataBaseService dataBaseService, IFilesServices filesServices) : base(navigationService)
        {
            this.dataBaseService = dataBaseService;
            this.filesServices = filesServices;

            MotivosList = this.dataBaseService.Get().DesVarMovimientosTipos.ToObservableCollection();
        }

        [RelayCommand]
        public async Task TakePhoto()
        {
            var result = await Application.Current.MainPage.DisplayActionSheet("Seleccione una opcion", "Cancelar", null, "Tomar Fotografia", "Elegir desde Galeria");
            FileResult photo = null;
            if (result == "Tomar Fotografia")
                photo = await MediaPicker.Default.CapturePhotoAsync();
            else if (result == "Elegir desde Galeria")
                photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo == null) return;

            Url = photo.FullPath;
        }

        [RelayCommand]
        public async Task DesabilitarOrigen()
        {
            if(SelectedMotivo == null|| PhotoComment.IsNullOrEmpty() || Url.IsNullOrEmpty())
            {
                Application.Current?.MainPage?.DisplayAlert("Error 401", "Necesitas llenar correctamente los datos", "Entendido");
                return;
            }

            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Realmente deseas desabilitar este registro?", "Aceptar", "Cancelar");
            if (!result)
            {
                WeakReferenceMessenger.Default.Send(new EliminarOrigenMessage(false, plantaDesarrollo.IdDesarrollo.ToString()));
                await Navigation.PopAsync();
                return;
            }

            var db = dataBaseService.Get();
            var ultimoMovimiento = db.DesVarMovimientos.Where(x => x.IdDesarrollo == plantaDesarrollo.IdDesarrollo && x.TipoAccion == null).OrderByDescending(x => x.FechaRegistro).FirstOrDefault();
            db.DesVarMovimientos.Where(x => x.IdDesarrollo == plantaDesarrollo.IdDesarrollo && x.TipoAccion != null).ExecuteDelete();
            var imageName = await filesServices.UploadImagen(Url);

            if (ultimoMovimiento == null || ultimoMovimiento.Accion == "A")
            {
                var imagen = new DesVarMovimientosApp()
                {
                    IdDesarrollo = plantaDesarrollo.IdDesarrollo,
                    UrlImagen = Path.Combine("Movimientos", plantaDesarrollo.IdDesarrollo.ToString(), imageName),
                    Comentario = PhotoComment,
                    IdTipo = SelectedMotivo.IdMovimientoTipo,
                    Accion = "D",
                    Descargada = true,
                    TipoAccion = "A",
                    FullPath = Path.Combine(AppSettings.ImagesUrl, imageName)
                };
                db.DesVarMovimientos.Add(imagen);
                db.SaveChanges();
            }

            var planta = db.HistDesarrolloVariedades.SingleOrDefault(x => x.IdDesarrollo == plantaDesarrollo.IdDesarrollo);
            planta.TipoAccion = "D";
            planta.Activo = false;
            await db.SaveChangesAsync();

            await Application.Current.MainPage.DisplayAlert("Desabilitado Exitoso", "Su registro fue desabilitado exitosamente.", "Entendido");

            WeakReferenceMessenger.Default.Send(new EliminarOrigenMessage(true, plantaDesarrollo.IdDesarrollo.ToString()));
            MainThread.BeginInvokeOnMainThread(async () => { await Navigation.PopAsync(); });
        }
    }
}