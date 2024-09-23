using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class DesVarMovimientosTipos
    {
        [Key]
        public int IdMovimientoTipo { get; set; }
        public string Descripcion { get; set; }
    }
}
