namespace DesarrolloVariedadesApp.Messages
{
    public class EliminarOrigenMessage
    {
        public bool Respuesta { get; set; }
        public string ValorBuscado { get; set; }

        public EliminarOrigenMessage(bool Respuesta, string ValorBuscado)
        {
            this.Respuesta = Respuesta;
            this.ValorBuscado = ValorBuscado;  
        }
    }
}