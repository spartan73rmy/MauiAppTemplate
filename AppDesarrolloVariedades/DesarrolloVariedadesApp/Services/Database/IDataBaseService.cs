using DesarrolloBVF.App.DataBase;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloBVF.App.Domain.Enums;

namespace DesarrolloVariedadesApp.Services.Database
{
    public interface IDataBaseService
    {
        public DesarrolloDbContext Get();

        EEstatusBD DbStatus { get; }

        public DateTime UltimaActualizacion { get; set; }
        public Task ActualizarBD();

        public Task<string> ActualizarCatalogos();

        public Task SubirData();

        public string? SetPresentacionJSON(ProcPresentacionEmpApp presentacion, char accion);

        public string? SetVariedadJSON(CatProductosApp variedad, char accion);

        public string? SetProductoJSON(ProcCatProductosApp producto, char accion);

        public string? SetCultivoJSON(CatTiposProdApp cultivo);

        public int NuevosRegistros();

        public int RegistrosActualizados();

        public Task<string?> GetJsonDesarrolloUbicaciones(string tipoAccion);

        public Task<string?> GetJsonUbicaciones(string tipoAccion);

        public Task<string?> GetJsonDesarrollo(string tipoAccion);
        public Task<string?> GetJsonImagenes(string tipoAccion);
    }
}
