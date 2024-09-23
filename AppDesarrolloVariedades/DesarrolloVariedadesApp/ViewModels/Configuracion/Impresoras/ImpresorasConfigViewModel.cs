using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Services.Impresoras;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.Configuracion.Impresoras
{
    public partial class ImpresorasConfigViewModel : BaseViewModel
    {
        private readonly IImpresoraConfigService impresorasConfigService;

        [ObservableProperty]
        private ObservableCollection<BluetoothDevice> bluetoothDevices = new ObservableCollection<BluetoothDevice>();

        [ObservableProperty]
        private string selectedImpresora;

        public ImpresorasConfigViewModel(INavigationService navigationService, IImpresoraConfigService impresorasConfigService) : base(navigationService)
        {
            this.impresorasConfigService = impresorasConfigService;
            SelectedImpresora = this.impresorasConfigService.ImpresoraName;
        }

        [RelayCommand]
        private async Task StartScanner()
        {
            try
            {
#if ANDROID
                Android.Bluetooth.BluetoothAdapter bluetoothAdapter = Android.Bluetooth.BluetoothAdapter.DefaultAdapter;
                if (bluetoothAdapter.IsEnabled == false){
                var enable = new Android.Content.Intent(
                        Android.Bluetooth.BluetoothAdapter.ActionRequestEnable);
                        enable.SetFlags(Android.Content.ActivityFlags.NewTask);
                        Android.App.Application.Context.StartActivity(enable);
                }
                if (bluetoothAdapter == null || !bluetoothAdapter.IsEnabled){
                     var  r = Enumerable.Empty<BluetoothDevice>();
                }
                else{
                    var pairedDevices = bluetoothAdapter.BondedDevices;
                    BluetoothDevices.Clear();
                    foreach(var device in pairedDevices){
                        BluetoothDevices.Add(new BluetoothDevice {Name = device.Name , Address = device.Address });
                    }
                }
#endif
            }
            catch (Exception x)
            {
                Application.Current.MainPage.DisplayAlert("Permisos no otorgados", "Por favor verifique que los permios fueron otorgados", "Aceptar");
            }
        }

        [RelayCommand]
        private void Select(object sender)
        {
            if (sender is Button button)
                if (button.BindingContext is BluetoothDevice impresora)
                {
                    impresorasConfigService.Impresora = impresora.Address;
                    impresorasConfigService.ImpresoraName = impresora.Name;
                    SelectedImpresora = impresora.Name;
                }
        }
    }
}