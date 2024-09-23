using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class ProcCatProductosSegmentos
    {
        [Key]
        public int CodProducto { get; set; }
        public int Codigo { get; set; }
        public int SubCodigo { get; set; }
    }
}
