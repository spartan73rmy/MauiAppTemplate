using DesarrolloBVF.App.Domain.Entities.Common;
using Season.DesarrolloBVF.Domain.Entities;
using Newtonsoft.Json;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class DesVarUbicacionRelativaApp : DesVarUbicacionRelativa, MobileEntityBase
    {
        public string? TipoAccion { get; set; }
        [JsonIgnore]
        public HistDesarrolloVariedadesApp Desarrollo{ get; set;}
        public DesVarUbicacionRelativaApp()
        { }
        public DesVarUbicacionRelativaApp(DesVarUbicacionRelativa ubicacion)
        {
            IdUbicacion = ubicacion.IdUbicacion;
            IdDesarrollo = ubicacion.IdDesarrollo; 
            Valor = ubicacion.Valor; 
            Descripcion = ubicacion.Descripcion; 
            Tipo = ubicacion.Tipo; 
            Activo = ubicacion.Activo; 

        }
    }
}
