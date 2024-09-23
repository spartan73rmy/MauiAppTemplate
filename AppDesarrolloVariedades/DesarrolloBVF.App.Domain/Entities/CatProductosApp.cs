using Season.DesarrolloBVF.Domain.Entities;
using Newtonsoft.Json;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class CatProductosApp : CatProductos
    {
        [JsonIgnore]
        public CatTiposProdApp Cultivo { get; set; }

        [JsonIgnore]
        public ICollection<ProcCatProductosApp> Produtos { get; set; }

        public CatProductosApp()
        {}

        public CatProductosApp(CatProductos productos)
        {
            Tipo = productos.Tipo;
            Producto = productos.Producto;
            Descripcion = productos.Descripcion;
            DescripcionIngles = productos.DescripcionIngles;
            Organico = productos.Organico;
            Abreviacion = productos.Abreviacion;
            NombreInterno = productos.NombreInterno;
        }
    }
}
