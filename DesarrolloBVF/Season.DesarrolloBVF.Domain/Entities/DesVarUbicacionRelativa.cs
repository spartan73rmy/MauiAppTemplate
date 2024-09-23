using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class DesVarUbicacionRelativa
    {
        [Key]
        public int IdUbicacion { get; set; }
        public long IdDesarrollo { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; } = true;
    }
}
