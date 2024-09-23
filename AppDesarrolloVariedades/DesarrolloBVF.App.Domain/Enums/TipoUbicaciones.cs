namespace DesarrolloBVF.App.Domain.Enums
{
    public enum UbicacionesOrigen
    {
        Zona,
        Sector,
        Tunel,
        Cama,
        Segmento
    }

    public static class UbicacionesOrigenExtencion
    {
        public static string GetValor(this UbicacionesOrigen tipo)
        {
            switch (tipo)
            {
                case UbicacionesOrigen.Zona:
                    return "Z";
                case UbicacionesOrigen.Sector:
                    return "SC";
                case UbicacionesOrigen.Tunel:
                    return "T";
                case UbicacionesOrigen.Cama:
                    return "C";
                case UbicacionesOrigen.Segmento:
                    return "S";
                default:
                    throw new ArgumentException($"Tipo de ubicación no válido: {tipo}", nameof(tipo));
            }
        }
    }
}
