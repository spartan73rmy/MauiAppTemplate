using DesarrolloBVF.App.Domain.Enums;

namespace DesarrolloVariedadesApp.Services.Impresoras
{
    public interface IImpresoraConfigService
    {
        public string Impresora { get; set; }
        public string ImpresoraName { get; set; }

        public Task<bool> ImprimirEtiqueta(string tipo, string[] payload, string[] qrTxt);
    }
}
