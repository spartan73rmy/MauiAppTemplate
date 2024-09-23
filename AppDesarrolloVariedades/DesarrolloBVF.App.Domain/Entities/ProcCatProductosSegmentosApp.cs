using Newtonsoft.Json;
using Season.DesarrolloBVF.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class ProcCatProductosSegmentosApp : ProcCatProductosSegmentos
    {
        [NotMapped]
        public string NombreCodigo => $"{Codigo}-{SubCodigo}";

        [JsonIgnore]
        public CatCentrosCostos CentroCosto { get; set; }

        public ProcCatProductosSegmentosApp()
        { }

        public ProcCatProductosSegmentosApp(ProcCatProductosSegmentos catProductosSegmentos)
        {
            CodProducto = catProductosSegmentos.CodProducto;
            Codigo = catProductosSegmentos.Codigo;
            SubCodigo = catProductosSegmentos.SubCodigo;
        }
    }
}
