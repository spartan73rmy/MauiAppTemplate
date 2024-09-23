using Season.DesarrolloBVF.Domain.Models;

namespace DesarrolloVariedadesApp.Services.ApiDatabase
{
    public interface IApiDatabaseService
    {
        public Task<T> GetRequest<T>(string endpint);

        public Task<T> PostRequest<T>(string endpint, string body);

        public Task<string> PostRequest(string endpint, string body);

        public Task<string> PostRequest(string endpint, HttpContent body);

        public Task<string> IniciarSesion(string username, string password);
        public Task<VersionModel> GetVersionApp();
    }
}
