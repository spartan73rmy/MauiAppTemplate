namespace DesarrolloBVF.App.Domain.Entities
{
    public class LogModel
    {
        public string UsuarioId { get; set; }
        public string UsuarioName { get; set; }
        public string Accion { get; set; }
        public string Tabla { get; set; }
        public string Descripcion { get; set; }
        public string IdAfectado { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
