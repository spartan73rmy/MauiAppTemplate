using Season.DesarrolloBVF.Domain.Entities;
using Newtonsoft.Json;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class ProcPresentacionEmpApp: ProcPresentacionEmp
    {
        [JsonIgnore]
        public PresentacionesAux? TipoPresetacion { get; set; }
        [JsonIgnore]
        public UnidadesAux? Unidad { get; set; }

        public ProcPresentacionEmpApp()
        { }

        public ProcPresentacionEmpApp(ProcPresentacionEmp presentacion)
        {
            CodPreEmp = presentacion.CodPreEmp; 
            IdPresentacion = presentacion.IdPresentacion; 
            IdUnidad = presentacion.IdUnidad; 
            Descripcion = presentacion.Descripcion;
            Factor = presentacion.Factor;
        }
    }
}
