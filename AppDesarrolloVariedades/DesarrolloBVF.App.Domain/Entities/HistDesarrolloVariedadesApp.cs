using DesarrolloBVF.App.Domain.Entities.Common;
using Season.DesarrolloBVF.Domain.Entities;
using Newtonsoft.Json;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class HistDesarrolloVariedadesApp : HistDesarrolloVariedades, MobileEntityBase
    {
        public string? TipoAccion { get; set; }

        [JsonIgnore]
        public ICollection<DesVarUbicacionRelativaApp> Ubicaciones { get; set; }
        [JsonIgnore]
        public DesVarTipos TipoDesarrollo { get; set; }
        [JsonIgnore]
        public ICollection<DesVarImagenesApp> Imagenes { get; set; }

        [JsonIgnore]
        public ProcCatProductosApp Producto { get; set; }
        [JsonIgnore]
        public ProcPresentacionEmpApp Presentacion { get; set; }
        [JsonIgnore]
        public HistDesarrolloVariedadesApp? ReferenciaPadre { get; set; }

        public HistDesarrolloVariedadesApp()
        { }
        public HistDesarrolloVariedadesApp(HistDesarrolloVariedades desarrollo)
        {
            IdDesarrollo = desarrollo.IdDesarrollo;
            FechaRegistro = desarrollo.FechaRegistro;
            Consecutivo = desarrollo.Consecutivo;
            IdProducto = desarrollo.IdProducto;
            IdPresentacion = desarrollo.IdPresentacion;
            Lote = desarrollo.Lote;
            IdTipoDesarrollo = desarrollo.IdTipoDesarrollo;
            IdReferenciaPadre = desarrollo.IdReferenciaPadre;
            Activo = desarrollo.Activo;
            Individuo = desarrollo.Individuo;
        }
    }
}
