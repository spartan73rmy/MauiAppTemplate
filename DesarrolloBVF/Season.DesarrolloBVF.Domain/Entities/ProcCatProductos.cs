using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class ProcCatProductos
    {
        [Key]
        public int CodProducto { get; set; }
        public int Producto { get; set; }
        public int Tipo { get; set; }
        public int? TipoPlanta { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; } = string.Empty;
        public string? CodProdServSAT { get; set; } = "10152001";
        public string? CodUniMedSAT { get; set; } = "PIEZA";
        public bool Activo { get; set; } = true;
        public bool SolicitudFinanciamiento { get; set; } = false;
        public string SKU { get; set; }
        public string CodigoSap { get; set; } = string.Empty;

    }
}
