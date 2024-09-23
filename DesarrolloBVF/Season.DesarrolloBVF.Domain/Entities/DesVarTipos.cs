using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class DesVarTipos
    {
        [Key]
        public int IdTipoDesarrollo { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }
        public bool Activo { get; set; } = true;
    }
}
