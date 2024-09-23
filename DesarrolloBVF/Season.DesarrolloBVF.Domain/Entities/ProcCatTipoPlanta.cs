using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class ProcCatTipoPlanta
    {
        [Key]
        public int TipoPlanta {  get; set; }
        public string Descripcion { get; set; }
    }
}
