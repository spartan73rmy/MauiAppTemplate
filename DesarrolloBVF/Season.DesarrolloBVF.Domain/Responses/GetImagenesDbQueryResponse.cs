using Season.DesarrolloBVF.Domain.Entities.VariantEntities;

namespace Season.DesarrolloBVF.Domain.Responses
{
    public class GetImagenesDbQueryResponse
    {
        public GetImagenesDbQueryResponse(IEnumerable<DesVarImagenes> desVarImagenes)
        {
            DesVarImagenes = desVarImagenes;
        }

        public IEnumerable<DesVarImagenes> DesVarImagenes { get; }
    }
}
