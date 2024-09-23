using DesarrolloBVF.App.Domain.Entities;
using Season.DesarrolloBVF.Domain.Entities.VariantEntities;

namespace DesarrolloVariedadesApp.Services.Files
{
    public interface IFilesServices
    {
        Task<string> UploadImagen(string url, string nombre = null);

        string GetUrlImage(string imageName);

        Task SubirImagenAlServidor<T>(T imagen);
        public Task DescargarImagenesDelServidor(IEnumerable<string> urls);
    }
}
