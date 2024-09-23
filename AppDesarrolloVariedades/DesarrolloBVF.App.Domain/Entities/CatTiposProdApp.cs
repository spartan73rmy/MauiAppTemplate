using Season.DesarrolloBVF.Domain.Entities;
using Newtonsoft.Json;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class CatTiposProdApp : CatTiposProd
    {
        [JsonIgnore]
        public ICollection<CatProductosApp> Variedades { get; set; }
        public CatTiposProdApp()
        {}
        public CatTiposProdApp(CatTiposProd catTiposProd)
        {
            Tipo = catTiposProd.Tipo;
            Descripcion = catTiposProd.Descripcion;
            DescIngles = catTiposProd.DescIngles;
        }
    }
}
