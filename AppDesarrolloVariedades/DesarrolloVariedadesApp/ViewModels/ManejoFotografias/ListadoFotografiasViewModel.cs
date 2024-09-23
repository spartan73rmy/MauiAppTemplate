using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Messages;
using DesarrolloVariedadesApp.Security;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using DesarrolloVariedadesApp.Views.ManejoFotografias;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.ManejoFotografias
{
    public partial class ListadoFotografiasViewModel : BaseViewModel
    {
        private readonly IDataBaseService dataBaseService;
        private readonly ICurrentUserAppService user;
        
        [ObservableProperty]
        private ObservableCollection<DesVarImagenesApp> imagenesList = new();

        [ObservableProperty]
        private string url = "", photoComment = "";

        [ObservableProperty]
        private bool permisoModificar, estadoModificar, estadoBase, noImagenes;

        public HistDesarrolloVariedadesApp plantaDesarrollo = new();

        public ListadoFotografiasViewModel(INavigationService navigationService, IDataBaseService dataBaseService, ICurrentUserAppService user) : base(navigationService)
        {
            this.dataBaseService = dataBaseService;
            this.user = user;

            PermisoModificar = user.Roles.Contains(PermisosDesarrolloVariedad.ImagenesEditar);
        }

        public void LoadData()
        {
            var db = dataBaseService.Get();
            var listaImagenes = db.DesVarImagenes.Where(x => x.IdDesarrollo == plantaDesarrollo.IdDesarrollo && x.TipoAccion != "D").ToList();
            foreach (var imagen in listaImagenes)
                imagen.Descargada = false;
            
            ImagenesList.Clear();
            ImagenesList = listaImagenes.ToObservableCollection();

            if(ImagenesList.Count() > 0)
                EstadoBase = true;
            else
            {
                NoImagenes = true;
                EstadoBase = false;
                PermisoModificar = false;
            }
        }

        [RelayCommand]
        public void CambioModificar()
        {
            if (!PermisoModificar)
            {
                Application.Current?.MainPage?.DisplayAlert("Advertencia", "No cuantas con el permiso para modificar estas imagenes.", "Entendido");
                return;
            }
            EstadoBase = false;
            EstadoModificar = true;
        }

        [RelayCommand]
        public async Task RegresarCambios()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea regresar los cambios\na su estado original?", "Aceptar", "Cancelar");
            if (!result)
                return;

            EstadoBase = true;
            EstadoModificar = false;
            LoadData();
        }

        [RelayCommand]
        public async Task EliminarFoto(object obj)
        {
            if (obj is ImageButton button)
                if (button.BindingContext is DesVarImagenesApp foto)
                {
                    var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea eliminar esta fotografia?", "Aceptar", "Cancelar");
                    if (!result)
                        return;

                    var index = ImagenesList.IndexOf(foto);
                    var fotoEliminada = new DesVarImagenesApp()
                    {
                        IdImagen = foto.IdImagen,
                        Comentario = foto.Comentario,
                        FullPath = foto.FullPath,
                        Activo = false,
                        TipoAccion = "D",
                        Descargada = true
                    };
                    ImagenesList[index] = fotoEliminada;
                }
        }

        [RelayCommand]
        public async Task HabilitarFoto(object obj)
        {
            if (obj is ImageButton button)
                if (button.BindingContext is DesVarImagenesApp foto)
                {
                    var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea habilitar esta fotografia?", "Aceptar", "Cancelar");
                    if (!result)
                        return;

                    var db = dataBaseService.Get();
                    var imgOG = db.DesVarImagenes.Where(x => x.IdImagen == foto.IdImagen).SingleOrDefault();
                    var index = ImagenesList.IndexOf(foto);

                    if (imgOG.Comentario != foto.Comentario || imgOG.FullPath != foto.FullPath)
                    {
                        var fotoModificada = new DesVarImagenesApp()
                        {
                            UrlImagen = imgOG.UrlImagen,
                            IdImagen = imgOG.IdImagen,
                            TipoAccion = imgOG.TipoAccion,
                            Comentario = foto.Comentario,
                            FullPath = foto.FullPath,
                            Activo = true,
                            Descargada = false
                        };
                        ImagenesList[index] = fotoModificada;
                    }
                    else
                    {
                        imgOG.Descargada = false;
                        ImagenesList[index] = imgOG;
                    }
                }
        }

        [RelayCommand]
        public async Task GuardarImagenes()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Desea guardar cambios\nsobre estas imagenes?", "Aceptar", "Cancelar");
            if (!result)
                return;

            var cambio = false;
            var cambioimg = false;

            var db = dataBaseService.Get();
            var imgOG = db.DesVarImagenes.Where(x => x.IdDesarrollo == plantaDesarrollo.IdDesarrollo && x.TipoAccion != "D").ToList();

            foreach (var i in imgOG)
                foreach (var j in ImagenesList)
                    if (i.IdImagen == j.IdImagen)
                    {
                        if (j.TipoAccion.IsNullOrEmpty() || j.TipoAccion == "M")
                            if (i.Comentario != j.Comentario)
                                j.TipoAccion = "M";
                        break;
                    }

            foreach (var img in ImagenesList)
            {
                if (img.TipoAccion == "D")
                {
                    cambio = true;
                    var imgDel = db.DesVarImagenes.SingleOrDefault(x => x.IdImagen == img.IdImagen);
                    imgDel.Activo = img.Activo;
                    imgDel.TipoAccion = img.TipoAccion;
                    db.SaveChanges();
                }
                if (img.TipoAccion == "M" || img.TipoAccion == "A")
                {
                    var imgMod = db.DesVarImagenes.SingleOrDefault(x => x.IdImagen == img.IdImagen);
                    if (imgMod.Comentario != img.Comentario)
                    {
                        cambioimg = true;
                        imgMod.Comentario = img.Comentario;
                    }
                    if (cambioimg)
                    {
                        cambio = true;
                        cambioimg = false;
                        imgMod.TipoAccion = img.TipoAccion;
                        db.SaveChanges();
                    }
                }
            }
            if (cambio)
                await Application.Current.MainPage.DisplayAlert("Modificaion Exitosa", "Las imagenes fueron modificadas\nexitosamente.", "Entendido");
            else
                await Application.Current.MainPage.DisplayAlert("Advertencia", "No se encontraron imagenes modificadas.", "Entendido");

            WeakReferenceMessenger.Default.Send(new FotografiasModificadasMessage(cambio, plantaDesarrollo.IdDesarrollo.ToString()));

            EstadoBase = true;
            EstadoModificar = false;
            LoadData();
        }

        [RelayCommand]
        public void ImagenClicked(object obj)
        {
            if (obj is Image imagen)
                if (imagen.BindingContext is DesVarImagenesApp img)
                {
                    var currentPage = Application.Current.MainPage;
                    if (currentPage is Page page)
                        page.ShowPopup(new PopupImagenZoom(new PopupImagenZoomViewModel(Navigation, img)));
                }
        }
    }
}