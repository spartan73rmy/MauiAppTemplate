using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class HistDesarrolloVariedades
    {
        [Key]
        public long IdDesarrollo { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int Consecutivo { get; set; }
        public int IdProducto { get; set; }
        public int IdPresentacion { get; set; }
        public string Lote { get; set; }
        public int IdTipoDesarrollo { get; set; }
        public long? IdReferenciaPadre { get; set; }
        public bool Activo { get; set; } = true;
        public int Individuo { get; set; }

    }
}
