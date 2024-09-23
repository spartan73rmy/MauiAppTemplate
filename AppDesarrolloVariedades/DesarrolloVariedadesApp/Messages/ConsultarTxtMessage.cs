using DesarrolloBVF.App.Domain.Enums;

namespace DesarrolloVariedadesApp.Messages
{
    public class ConsultarTxtMessage
    {
        public string ValorBuscado { get; set; }
        public TiposConsula Tipo { get; set; }

        public ConsultarTxtMessage(string valorBuscado, TiposConsula tipo)
        {
            ValorBuscado = valorBuscado;
            Tipo = tipo;
        }
    }
}
