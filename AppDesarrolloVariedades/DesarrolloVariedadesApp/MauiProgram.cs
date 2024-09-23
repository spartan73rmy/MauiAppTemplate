using CommunityToolkit.Maui;
using DesarrolloVariedadesApp.Controls;
using DesarrolloVariedadesApp.Services.ApiDatabase;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Files;
using DesarrolloVariedadesApp.Services.Impresoras;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.Services.Report;
using DesarrolloVariedadesApp.ViewModels;
using DesarrolloVariedadesApp.ViewModels.Common;
using DesarrolloVariedadesApp.ViewModels.Configuracion.Impresoras;
using DesarrolloVariedadesApp.ViewModels.Edicion.Cultivo;
using DesarrolloVariedadesApp.ViewModels.ManejoFotografias;
using DesarrolloVariedadesApp.Views;
using DesarrolloVariedadesApp.Views.Common;
using DesarrolloVariedadesApp.Views.Configuracion;
using DesarrolloVariedadesApp.Views.Configuracion.Impresoras;
using DesarrolloVariedadesApp.Views.Edicion;
using DesarrolloVariedadesApp.Views.Edicion.CultivoVariedad;
using DesarrolloVariedadesApp.Views.ManejoFotografias;
using Material.Components.Maui.Extensions;
using Microsoft.Maui.Handlers;
using ZXing.Net.Maui.Controls;

namespace DesarrolloVariedadesApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(CustomEntry), typeof(EntryHandler));
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMaterialComponents()
                .UseBarcodeReader();
            EntryHandler.Mapper.AppendToMapping(nameof(CustomEntry), (handler, view) =>
            {
#if ANDROID

                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif WINDOWS
                var nativeEntry = handler.PlatformView as Microsoft.UI.Xaml.Controls.TextBox;

                if (nativeEntry != null)
                {
                    nativeEntry.BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                    nativeEntry.Resources["TextControlBorderBrushPointerOver"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                    nativeEntry.Resources["TextControlBorderBrushFocused"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                    nativeEntry.Resources["TextControlBorderBrushDisabled"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                    nativeEntry.Resources["TextControlBorderBrush"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                }
                Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping("NoLabel", (handler, View) =>
                {
                    handler.PlatformView.OnContent = null;
                    handler.PlatformView.OffContent = null;

                });
#endif



            });
            builder.RegistrarServicios();
            builder.RegistrarViewModels();
            builder.RegistrarViews();

            return builder.Build();
        }

        public static MauiAppBuilder RegistrarServicios(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IDataBaseService, DatabaseService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddTransient<IApiDatabaseService, ApiDatabaseService>();
            builder.Services.AddSingleton<ICurrentUserAppService, CurrentUserAppService>();
            builder.Services.AddSingleton<IImpresoraConfigService, ImpresoraConfigService>();
            builder.Services.AddSingleton<IReportService, ReportService>();
            builder.Services.AddSingleton<IFilesServices, FilesServices>();
            return builder;
        }

        public static MauiAppBuilder RegistrarViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddTransient<CameraCodeViewModel>();
            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<PopupImagenZoomViewModel>();
            builder.Services.AddTransient<AddPhotosViewModel>();
            builder.Services.AddTransient<ListadoFotografiasViewModel>();
            builder.Services.AddTransient<HistorialDesarrolloViewModel>();

            builder.Services.AddTransient<DisableOrigenViewModel>();

            builder.Services.AddSingleton<ConfiguracionViewModel>();
            builder.Services.AddTransient<ImpresorasConfigViewModel>();

            builder.Services.AddSingleton<EdicionViewModel>();
            builder.Services.AddTransient<CultivoVariedadBaseViewModel>();
            builder.Services.AddTransient<CultivoVariedadListadoViewModel>();
            builder.Services.AddTransient<CultivoVariedadNuevoViewModel>();
            builder.Services.AddTransient<CultivoVariedadModificarViewModel>();

            return builder;
        }

        public static MauiAppBuilder RegistrarViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddTransient<CameraCode>();
            builder.Services.AddTransient<HomeView>();

            builder.Services.AddTransient<AddPhotosView>();
            builder.Services.AddTransient<ListadoFotografiasView>();
            builder.Services.AddTransient<HistorialDesarrolloView>();

            builder.Services.AddTransient<DisableOrigenView>();

            builder.Services.AddSingleton<ConfiguracionMain>();
            builder.Services.AddTransient<ImpresorasConfigView>();

            builder.Services.AddSingleton<EdicionMain>();
            builder.Services.AddTransient<CultivoVarBase>();
            builder.Services.AddTransient<CultivoVarListado>();
            builder.Services.AddTransient<CultivoVarNuevo>();
            builder.Services.AddTransient<CultivoVarModificar>();

            return builder;
        }
    }
}
