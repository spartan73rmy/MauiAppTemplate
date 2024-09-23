namespace DesarrolloBVF.App.Domain.Enums
{
    public static class TiposDesarrolloVariedadExtencion
    {
        public static string GetValor(this TiposDesarrolloVariedad tipo)
        {
            return tipo switch
            {
                TiposDesarrolloVariedad.ORIGEN => "O",
                TiposDesarrolloVariedad.NODO => "N",
                TiposDesarrolloVariedad.Inv => "Inv",
                TiposDesarrolloVariedad.Genetica => "Gen",
                _ => throw new ArgumentException($"Tipo de desarrollo no válido: {tipo}", nameof(tipo)),
            };
        }
    }
    public enum TiposDesarrolloVariedad
    {
        ORIGEN = 1,
        NODO = 2,
        Inv = 3,
        Genetica = 4,
    }
}
