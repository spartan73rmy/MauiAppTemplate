using CommunityToolkit.Maui.Views;
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
using DesarrolloVariedadesApp.Views.ManejoFotografias;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.ManejoFotografias
{
    public partial class AddPhotosViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<DesVarImagenesApp> imagenesList = new();

        [ObservableProperty]
        private string url = "", photoComment = "";

        [ObservableProperty]
        private bool hayComentarios, puedeGuardar = false, puedeAgregar = false;

        public bool isListaTemporal = false;

        public HistDesarrolloVariedadesApp desarrolloPlanta = new();

        private readonly IDataBaseService dataBaseService;
        private readonly IFilesServices filesServices;

        public TiposConsula Tipo { get; set; }

        public AddPhotosViewModel(INavigationService navigationService, IDataBaseService dataBaseService, IFilesServices filesServices) : base(navigationService)
        {
            this.dataBaseService = dataBaseService;
            this.filesServices = filesServices;
        }

        private void Limipiar()
        {
            Url = "";
            PhotoComment = "";
        }

        [RelayCommand]
        public async Task GuardarImagenes()
        {
            if (!PuedeGuardar)
            {
                Application.Current?.MainPage?.DisplayAlert("Inavalido", "No as agregado ninguna imagen para guardar", "Entendido");
                return;
            }
            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea guardar cambios\nsobre estas imagenes?", "Aceptar", "Cancelar");
            if (!result)
                return;

            if (!isListaTemporal)
            {
                var db = dataBaseService.Get();

                foreach (var img in ImagenesList)
                {
                    var imageName = await filesServices.UploadImagen(img.FullPath);
                    var imagen = new DesVarImagenesApp()
                    {
                        IdDesarrollo = desarrolloPlanta.IdDesarrollo,
                        IdVariedad = desarrolloPlanta.Producto.Producto,
                        IdCultivo = desarrolloPlanta.Producto.Tipo,
                        Comentario = img.Comentario,
                        TipoAccion = "A",
                        Descargada = true,
                        FullPath = Path.Combine(AppSettings.ImagesUrl, imageName),
                        Tipo = "Seguimiento"
                    };
                    imagen.UrlImagen = Path.Combine($"{imagen.IdCultivo}-{imagen.IdVariedad}", imagen.IdDesarrollo.ToString(), imageName);
                    db.DesVarImagenes.Add(imagen);
                }
                db.SaveChanges();
                await Application.Current.MainPage.DisplayAlert("Guardado Exitoso", "Las imagenes fueron agregadas\nexitosamente.", "Entendido");
            }

            WeakReferenceMessenger.Default.Send(new AgregarFotosMessage(ImagenesList, Tipo));
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
            });
        }

        [RelayCommand]
        public void AgregarFoto()
        {
            if (Url.IsNullOrEmpty())
            {
                Application.Current?.MainPage?.DisplayAlert("Error 401", "Debes de agregar por lo menos una\nfotografia para realizar el registro.", "Entendido");
                return;
            }
            if (!HayComentarios)
            { ImagenesList.Add(new DesVarImagenesApp() { FullPath = Url }); }
            else
            { ImagenesList.Add(new DesVarImagenesApp() { FullPath = Url, Comentario = PhotoComment }); }
            PuedeGuardar = true;
            PuedeAgregar = false;
            Limipiar();
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
            if (photo == null)
                return;
            PuedeAgregar = true;
            Url = photo.FullPath;
        }

        [RelayCommand]
        public async Task EliminarFoto(object obj)
        {
            if (obj is ImageButton button)
            {
                if (button.BindingContext is DesVarImagenesApp foto)
                {
                    var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea eliminar esta fotografia?", "Aceptar", "Cancelar");
                    if (!result)
                        return;
                    ImagenesList.Remove(foto);
                }
            }
            if (ImagenesList.Count <= 0)
            {
                PuedeGuardar = false;
            }
        }

        [RelayCommand]
        public async Task Regresar()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Quieres dejar esta vista?", "Aceptar", "Cancelar");
            if (!result)
                return;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
            });
        }

        [RelayCommand]
        public void ImagenClicked(object obj)
        {
            if (obj is Image imagen)
                if (imagen.BindingContext is DesVarImagenesApp img)
                {
                    var currentPage = Application.Current.MainPage;
                    if (currentPage is Page page)
                        if (HayComentarios)
                            page.ShowPopup(new PopupImagenZoom(new PopupImagenZoomViewModel(Navigation, img, null, true)));
                        else
                            page.ShowPopup(new PopupImagenZoom(new PopupImagenZoomViewModel(Navigation, img, null, false)));
                }
        }
    }
}
