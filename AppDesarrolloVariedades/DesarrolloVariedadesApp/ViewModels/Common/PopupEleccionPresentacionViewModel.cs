using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.Messages;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Navigation;
using DesarrolloVariedadesApp.ViewModels.Base;
using Season.DesarrolloBVF.Domain.Entities;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.ViewModels.Common
{
    public partial class PopupEleccionPresentacionViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<PresentacionesAux> tiposPresentacionesList = new(); //Cambia esto al moedelo que vayas a usar

        [ObservableProperty]
        private PresentacionesAux selectedTipoPresentacion; //Cambia esto al moedelo que vayas a usar y cambia el metodo

        [ObservableProperty]
        private ObservableCollection<ProcPresentacionEmpApp> factorPresentacionesList = new(); //Este tambien cambialo

        [ObservableProperty]
        private ProcPresentacionEmpApp selectedFactor; //Este tambien cambialo

        [ObservableProperty]
        private bool descripcionActivo;
        [ObservableProperty]
        private string tipoUnidad;

        private readonly Popup popup;
        private readonly IDataBaseService dataBaseService;
        public TiposConsula Tipo { get; set; }

        public PopupEleccionPresentacionViewModel(Popup popup, INavigationService navigationService, IDataBaseService dataBaseService) : base(navigationService)
        {
            TiposPresentacionesList.Clear();
            var db = dataBaseService.Get();
            var tiposConRegistros = db.ProcPresentacionEmps.GroupBy(x => x.IdPresentacion).Select(x => x.Key);

            TiposPresentacionesList = db.TiposPresentacion.Where(x => tiposConRegistros.Contains(x.IdPresentacion)).ToObservableCollection();


            DescripcionActivo = false;
            this.popup = popup;
            this.dataBaseService = dataBaseService;
        }

        partial void OnSelectedTipoPresentacionChanged(PresentacionesAux value)
        {
            if (value != null)
            {
                //Filtra con respecto a la tabla
                DescripcionActivo = true;
                FactorPresentacionesList.Clear();
                FactorPresentacionesList = dataBaseService.Get().ProcPresentacionEmps.Where(x => x.IdPresentacion == value.IdPresentacion).ToObservableCollection();
                if (FactorPresentacionesList.Count() == 1)
                {
                    SeleccionarPresentacion(FactorPresentacionesList.First());
                }
                TipoUnidad = FactorPresentacionesList.Select(x => x.Unidad.Descripcion).First();
            }
        }

        partial void OnSelectedFactorChanged(ProcPresentacionEmpApp value)
        {

            if (value != null)
                SeleccionarPresentacion(value);


        }

        private void SeleccionarPresentacion(ProcPresentacionEmpApp value)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await popup.CloseAsync();
                WeakReferenceMessenger.Default.Send(new SelectedPresentacionMessage(value.CodPreEmp, Tipo));
            });
        }
    }
}
