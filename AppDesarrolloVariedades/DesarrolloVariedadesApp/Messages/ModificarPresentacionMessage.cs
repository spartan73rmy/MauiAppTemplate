using DesarrolloBVF.App.Domain.Entities;

namespace DesarrolloVariedadesApp.Messages
{
    public class ModificarPresentacionMessage
    {
        public ProcPresentacionEmpApp ValorBuscado { get; set; }

        public ModificarPresentacionMessage(ProcPresentacionEmpApp valorBuscado)
        {
            ValorBuscado = valorBuscado;
        }
    }
}