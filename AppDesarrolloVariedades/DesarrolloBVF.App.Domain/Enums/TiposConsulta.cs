namespace DesarrolloBVF.App.Domain.Enums
{
    public static class TiposConsultaExtencion
    {
        public static string GetTipoBuscado(this TiposConsula tipo)
        {
            return tipo switch
            {
                TiposConsula.Origen => "O",
                TiposConsula.Nodo => "N",
                TiposConsula.Inv => "Inv",
                TiposConsula.OrigenN => "O",
                TiposConsula.OrigenL => "O",
                TiposConsula.NodoN => "O",
                TiposConsula.InvN => "N",
                TiposConsula.Trazabilidad => "T",
                TiposConsula.General => "G",
                _ => throw new ArgumentException($"Tipo de consulta no válido: {tipo}", nameof(tipo)),
            };
        }
    }

    public enum TiposConsula
    {
        Origen,
        Nodo,
        Inv,
        OrigenN,
        OrigenL,
        NodoN,
        InvN,
        Trazabilidad,
        General
    }
}
