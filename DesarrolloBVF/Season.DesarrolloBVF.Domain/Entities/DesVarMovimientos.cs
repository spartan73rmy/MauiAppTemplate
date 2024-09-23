namespace Season.DesarrolloBVF.Domain.Entities
{
    public class DesVarMovimientos
    {
        public int IdMovimiento { get; set; }
        public long IdDesarrollo { get; set; }
        public string? UrlImagen { get; set; }
        public string? Comentario { get; set; }
        public int IdTipo { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Accion { get; set; }

    }
}
