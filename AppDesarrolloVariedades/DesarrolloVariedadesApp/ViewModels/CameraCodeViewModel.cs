using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.Messages;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace DesarrolloVariedadesApp.ViewModels
{
    public partial class CameraCodeViewModel : BaseViewModel
    {
        [ObservableProperty]
        public CameraBarcodeReaderView cameraBarcode;

        private readonly IDataBaseService dataBaseService;

        public TiposConsula Tipo { get; set; }

        public CameraCodeViewModel(IDataBaseService dataBaseService, INavigationService navigationService) : base(navigationService)
        {
            CameraBarcode = new CameraBarcodeReaderView
            {
                Options = new BarcodeReaderOptions
                {
                    Formats = BarcodeFormats.All,
                    AutoRotate = true,
                    Multiple = true
                },

                CameraLocation
                = CameraLocation.Rear
            };
            this.dataBaseService = dataBaseService;
        }

        [RelayCommand]
        public void Cancel()
        {
            MainThread.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());

        }

        public async Task BarcodesDetected(string r)
        {
            string? clavetxt, id;
            CameraBarcode.IsDetecting = false;
            try
            {
                clavetxt = r.Split('\n').First(x => x.Contains("Codigo"));
                id = clavetxt.Split(' ')[1];
            }
            catch (Exception e)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El QR no es reconocido.", "Entendido");
                    CameraBarcode.IsDetecting = true;
                });
                return;
            }

            if (string.IsNullOrEmpty(id))
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current?.MainPage?.DisplayAlert("Error", "QR dañado", "Entendido");
                    CameraBarcode.IsDetecting = true;
                });

                return;
            }

            var db = this.dataBaseService.Get();
            var plantaOrigen = await db.HistDesarrolloVariedades
                .SingleOrDefaultAsync(x => x.Lote == id || x.IdDesarrollo.ToString() == id);

            if (plantaOrigen == null)
            {

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current?.MainPage?.DisplayAlert("Error", "Registro no encontrado", "Entendido");
                    CameraBarcode.IsDetecting = true;
                });

                return;
            }
            if (Tipo != TiposConsula.General)
            {
                WeakReferenceMessenger.Default.Send(new ConsultarTxtMessage(id, Tipo));
                MainThread.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            }
            _= BuscarData(id);
        }

        private async Task BuscarData(string valor)
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
    }
}
