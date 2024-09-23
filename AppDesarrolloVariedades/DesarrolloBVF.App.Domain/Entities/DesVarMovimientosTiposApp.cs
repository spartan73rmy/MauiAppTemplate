using Season.DesarrolloBVF.Domain.Entities;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class DesVarMovimientosTiposApp : DesVarMovimientosTipos
    {
        public DesVarMovimientosTiposApp()
        { }

        public DesVarMovimientosTiposApp(DesVarMovimientosTipos desVarMovimientosTipos)
        {
            IdMovimientoTipo = desVarMovimientosTipos.IdMovimientoTipo;
            Descripcion = desVarMovimientosTipos.Descripcion;
        }
    }
}