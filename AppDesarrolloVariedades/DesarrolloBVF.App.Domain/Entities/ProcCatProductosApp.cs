using Season.DesarrolloBVF.Domain.Entities;
using Newtonsoft.Json;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class ProcCatProductosApp : ProcCatProductos
    {
        [JsonIgnore]
        public CatProductosApp Variedad { get; set; }
        [JsonIgnore]
        public ProcCatTipoPlanta? PlantaTipo { get; set; }
        [JsonIgnore]
        public ProcCatProductosSegmentosApp? Segmento { get; set; }

        public ProcCatProductosApp()
        { }

        public ProcCatProductosApp(ProcCatProductos procCatProductos)
        {
            CodProducto = procCatProductos.CodProducto;
            Producto = procCatProductos.Producto;
            Tipo = procCatProductos.Tipo;
            TipoPlanta = procCatProductos.TipoPlanta;
            Descripcion = procCatProductos.Descripcion;
            Abreviatura = procCatProductos.Abreviatura;
            CodProdServSAT = procCatProductos.CodProdServSAT;
            CodUniMedSAT = procCatProductos.CodUniMedSAT;
            Activo = procCatProductos.Activo;
            SolicitudFinanciamiento = procCatProductos.SolicitudFinanciamiento;
            SKU = procCatProductos.SKU;
            CodigoSap = procCatProductos.CodigoSap;
        }
    }
}
