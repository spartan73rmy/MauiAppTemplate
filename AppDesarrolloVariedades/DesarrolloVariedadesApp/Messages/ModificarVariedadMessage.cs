using DesarrolloBVF.App.Domain.Entities;

namespace DesarrolloVariedadesApp.Messages
{
    public class ModificarVariedadMessage
    {
        public CatProductosApp ValorBuscado { get; set; }

        public ModificarVariedadMessage(CatProductosApp valorBuscado)
        {
            ValorBuscado = valorBuscado;
        }
    }
}