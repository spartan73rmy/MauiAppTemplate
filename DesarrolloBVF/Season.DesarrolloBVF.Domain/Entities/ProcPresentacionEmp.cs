using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class ProcPresentacionEmp
    {
        [Key]
        public int CodPreEmp { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidad { get; set; }
        public string Descripcion { get; set; } = default!;
        public int Factor { get; set; }
        public int Activo { get; set; }

    }
}
