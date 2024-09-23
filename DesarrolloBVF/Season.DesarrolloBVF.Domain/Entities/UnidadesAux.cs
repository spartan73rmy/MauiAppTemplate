using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class UnidadesAux
    {
        [Key]
        public int IdUnidad { get; set; }
        public string Descripcion { get; set; }
    }
}
