using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class DesVarCatSitios
    {
        [Key]
        public int IdSitio { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }
    }
}
