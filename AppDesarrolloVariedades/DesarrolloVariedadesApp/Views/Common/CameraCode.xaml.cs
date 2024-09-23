using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.ViewModels;
using ZXing.Net.Maui;

namespace DesarrolloVariedadesApp.Views.Common;

public partial class CameraCode : ContentPage, IQueryAttributable
{
    public CameraCode(CameraCodeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
#if ANDROID
        Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
#endif
    }

    protected async void BarcodesDetectedCommand(object sender, BarcodeDetectionEventArgs e)
    {
        Vibration.Vibrate(20);
        var viewmodel = (CameraCodeViewModel)BindingContext;
        await viewmodel.BarcodesDetected(e.Results[0].Value);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
#if ANDROID
        Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Unspecified;
#endif

    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Tipo"))
        {
            var viemodel = (CameraCodeViewModel)BindingContext;
            viemodel.Tipo = (TiposConsula)query["Tipo"];
        }
    }


}