using Season.DesarrolloBVF.Domain.Entities;

namespace Season.DesarrolloBVF.Domain.Responses
{
    public class GetCatCultivosQueryResponse
    {
        public IEnumerable<CatTiposProd> Cultivos { get; }
        public IEnumerable<CatProductos> Variedades { get; }

        public GetCatCultivosQueryResponse(IEnumerable<CatTiposProd> cultivos, IEnumerable<CatProductos> variedades)
        {
            Cultivos = cultivos;
            Variedades = variedades;
        }
    }
}
