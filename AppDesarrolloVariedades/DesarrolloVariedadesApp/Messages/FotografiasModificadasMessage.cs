namespace DesarrolloVariedadesApp.Messages
{
    public class FotografiasModificadasMessage
    {
        public bool Respuesta { get; set; }

        public string ValorBuscado { get; set; }

        public FotografiasModificadasMessage(bool Respuesta, string valorBuscado)
        {
            this.Respuesta = Respuesta;
            ValorBuscado = valorBuscado;
        }
    }
}