using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class PresentacionesAux
    {
        [Key]
        public int IdPresentacion { get; set; }
        public string Descripcion { get; set; }
    }
}
