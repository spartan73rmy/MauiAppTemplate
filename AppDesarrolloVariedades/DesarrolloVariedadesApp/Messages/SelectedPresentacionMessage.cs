using DesarrolloBVF.App.Domain.Enums;

namespace DesarrolloVariedadesApp.Messages
{
    public class SelectedPresentacionMessage
    {
        public int IdPresentacion { get; set; }
        public TiposConsula Tipo { get; set; }

        public SelectedPresentacionMessage(int IdPresentacion, TiposConsula tipo)
        {
            this.IdPresentacion = IdPresentacion;
            this.Tipo = tipo;
        }
    }
}