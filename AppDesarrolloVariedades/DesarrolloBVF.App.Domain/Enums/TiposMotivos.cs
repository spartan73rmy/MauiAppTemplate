namespace DesarrolloBVF.App.Domain.Enums
{
    public static class TiposMotivos
    {
        public static string getTipoMotivo(this TipoMotivo tipo)
        {
            switch (tipo)
            {
                case TipoMotivo.BAJA: return "BAJA";
                case TipoMotivo.CUARENTENA: return "CUARENTENA";
                case TipoMotivo.OTRO: return "OTRO";
                default: throw new ArgumentException($"Tipo de motivo no es valido: {tipo}", nameof(tipo));
            }
        }
    }

    public enum TipoMotivo
    {
        BAJA,
        CUARENTENA,
        OTRO
    }
}