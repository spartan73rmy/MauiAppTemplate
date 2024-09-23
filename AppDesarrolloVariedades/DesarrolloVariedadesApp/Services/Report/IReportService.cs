using DesarrolloBVF.App.Domain.Entities;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.Services.Report
{
    public interface IReportService
    {
        public Task DescargarReporteIndividual(HistDesarrolloVariedadesApp plantaOrigen, string ubicacion = "");
        public Task DescargarReporteExcel(ObservableCollection<HistDesarrolloVariedadesApp> plantas, string nam);
    }
}
