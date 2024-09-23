
using DesarrolloBVF.App.Domain.Enums;
using System.Text;

namespace DesarrolloVariedadesApp.Services.Impresoras
{
    public class ImpresoraConfigService: IImpresoraConfigService
    {
        public string Impresora {  get => Preferences.Default.Get(nameof(Impresora), ""); set=> Preferences.Default.Set(nameof(Impresora),value); }
        public string ImpresoraName { get => Preferences.Default.Get(nameof(ImpresoraName), ""); set => Preferences.Default.Set(nameof(ImpresoraName), value); }
       

        private readonly string uuidS = "00001101-0000-1000-8000-00805F9B34FB";

        private string EtiquetaOrigen(string[] payload, string[] qrTxt)
        {
            var zpl = "^XA";
            var puntoInicial = 50;
            foreach (var txt in payload)
            {
                zpl += $"^FO50,{puntoInicial}^A0N,50,50^FD{txt}^FS";
                puntoInicial += 61;
            }
            string contenidoQR = "";
            foreach (var txt in qrTxt)
            {
                contenidoQR += $"{txt}_0A";
            }
            zpl += $"^FO50,{puntoInicial}^BQN,2,10^FH^FDM,{contenidoQR}^FS^XZ";
            return zpl;
            //return $"^XA^FO50,50^A0N,50,50^FD{payload}^FS^FO50,90^BQN,2,10^FDM,{qrTxt}^FS^FO300,50^ADN,25^FD{payload}^FS^FO300,60^BQN,2,4^FDM,{qrTxt}^FS^FO50,370^ADN,25^FD{payload}^FS^FO180,320^BQN,2,4^FDM,{qrTxt}^FS^XZ";
        }

        private string EtiquetaNodo(string[] payload, string[] qrTxt)
        {
            var zpl = "^XA";
            var puntoInicial = 50;
            foreach (var txt in payload)
            {
                zpl += $"^FO50,{puntoInicial}^A0N,50,50^FD{txt}^FS";
                puntoInicial += 61;
            }
            zpl += $"^FO50,{puntoInicial}^BQN,2,8^FDM,{qrTxt}^FS^XZ";
            return zpl;        
        }

        public ImpresoraConfigService() { }

        public async Task<bool> ImprimirEtiqueta(string tipo, string[] payload, string[] qrTxt)
        {
            if (string.IsNullOrEmpty(Impresora))
            {
                Application.Current.MainPage.DisplayAlert("Error de Configuracion", "No se ha configurado ninguna impresora.", "Aceptar");
            }

            string zpl = "";
            switch (tipo)
            {
                case nameof(TiposDesarrolloVariedad.ORIGEN):
                    zpl = EtiquetaOrigen(payload, qrTxt);
                    break;      
                case nameof(TiposDesarrolloVariedad.NODO):
                    zpl = EtiquetaNodo(payload, qrTxt);
                    break;
                case nameof(TiposDesarrolloVariedad.Inv):
                    zpl = EtiquetaOrigen(payload, qrTxt);
                    break;
            }
            byte[] bytesZpl = UnicodeEncoding.UTF8.GetBytes(zpl);

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

            if (bluetoothAdapter != null || bluetoothAdapter.IsEnabled){
                var pairedDevices = bluetoothAdapter.BondedDevices;
                Android.Bluetooth.BluetoothDevice impresora = pairedDevices.FirstOrDefault(x => x.Address == Impresora);

                if(impresora == null){
                    return false;
                }
                using (Android.Bluetooth.BluetoothSocket socket = impresora.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(uuidS)))
                {
            
                    // Conectar con el dispositivo Bluetooth
                    if (!socket.IsConnected)
                    {
                        await socket.ConnectAsync();
                    }


                    // Escribir los bytes del archivo en el socket Bluetooth
                    await socket.OutputStream.WriteAsync(bytesZpl, 0, bytesZpl.Length);

                    // Cerrar la conexión Bluetooth
                    socket.Close();
                }
                return true;
            }

#endif
                return false;

            }
            catch (Exception ex) {
                Application.Current.MainPage.DisplayAlert("Problemas al imprimir", "Por favor verifique que los permios fueron otorgados y que esta habilitado el Bluetooth", "Aceptar");
                return false;
            }

        }



    }
}
