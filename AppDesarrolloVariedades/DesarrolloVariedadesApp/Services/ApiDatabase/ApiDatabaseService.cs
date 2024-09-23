using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using DesarrolloVariedadesApp.Services.CurrentUserApp;
using Season.DesarrolloBVF.Domain.Models;

namespace DesarrolloVariedadesApp.Services.ApiDatabase
{
    public class ApiDatabaseService : IApiDatabaseService
    {

        private readonly ICurrentUserAppService currentUserAppService;

        public ApiDatabaseService(ICurrentUserAppService currentUserAppService)
        {
            this.currentUserAppService = currentUserAppService;
        }

        private HttpClient Get(int timeoutSeconds, bool token = true)
        {
            var handler = new HttpClientHandler
            {
                // Desactivar la validación de OCSP
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(AppSettings.ApiDatabaseUri),
                Timeout = TimeSpan.FromSeconds(timeoutSeconds)
            };
            if (token)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentUserAppService.DbApiToken);
            return httpClient;
        }
        private async Task RevisarApi()
        {
            using var httpClient = Get(2, false);
            try
            {
                await httpClient.GetAsync("Public/Available");
            }
            catch
            {
                throw new OperationCanceledException("Servicio no encontrado, compruebe su conexion");
            }
        }
        public async Task<VersionModel> GetVersionApp()
        {
            await RevisarApi();
            using var httpClient = Get(10, false);
            HttpResponseMessage response = await httpClient.GetAsync("Public/GetVersionApp");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VersionModel>();
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al hacer la solicitud: {response.ReasonPhrase}. Contenido del error: {errorContent}");
            }
        }

        public async Task<T> GetRequest<T>(string endpint)
        {
            await RevisarApi();
            using var httpClient = Get(10);
            HttpResponseMessage response = await httpClient.GetAsync(endpint);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al hacer la solicitud: {response.ReasonPhrase}. Contenido del error: {errorContent}");
            }
        }

        public async Task<string> PostRequest(string endpint, string json)
        {
            await RevisarApi();
            using var httpClient = Get(10);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(endpint, contenido);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al hacer la solicitud: {response.ReasonPhrase}. Contenido del error: {errorContent}");
            }

        }

        public async Task<T> PostRequest<T>(string endpint, string json)
        {
            await RevisarApi();
            using var httpClient = Get(10);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(endpint, contenido);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al hacer la solicitud: {response.ReasonPhrase}. Contenido del error: {errorContent}");
            }

        }

        public async Task<string> IniciarSesion(string username, string password)
        {
            await RevisarApi();
            using var httpClient = Get(10, false);
            HttpResponseMessage response = await httpClient.PostAsync($"Public/Login?username={username}&password={password}", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(errorContent);
            }
        }

        public async Task<string> PostRequest(string endpint, HttpContent body)
        {
            await RevisarApi();
            using var httpClient = Get(10);
            HttpResponseMessage response = await httpClient.PostAsync(endpint, body);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();             
                throw new HttpRequestException($"Error al hacer la solicitud: {response.ReasonPhrase}. Contenido del error: {errorContent}");
            }
        }
    }
}
