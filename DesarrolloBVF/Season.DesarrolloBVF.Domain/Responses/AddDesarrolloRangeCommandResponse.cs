namespace Season.DesarrolloBVF.Domain.Responses
{
    public class AddDesarrolloRangeCommandResponse
    {
        public AddDesarrolloRangeCommandResponse(IEnumerable<long> errores)
        {
            Errores = errores;
        }

        public IEnumerable<long> Errores { get; }
    }
}
