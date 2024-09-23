using DesarrolloBVF.App.Domain.Entities;

namespace DesarrolloVariedadesApp.Messages
{
    public class ModificarProductoMessage
    {
        public ProcCatProductosApp ValorBuscado { get; set; }

        public ModificarProductoMessage(ProcCatProductosApp valorBuscado)
        {
            ValorBuscado = valorBuscado;
        }
    }
}