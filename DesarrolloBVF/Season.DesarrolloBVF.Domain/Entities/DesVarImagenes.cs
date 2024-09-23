namespace Season.DesarrolloBVF.Domain.Entities.VariantEntities
{
    public class DesVarImagenes
    {
        public int IdImagen { get; set; }
        public int IdCultivo { get; set; }
        public int IdVariedad { get; set; }
        public long? IdDesarrollo { get; set; }
        public string UrlImagen { get; set; }
        public string? Comentario { get; set; }
        public string? Tipo { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
