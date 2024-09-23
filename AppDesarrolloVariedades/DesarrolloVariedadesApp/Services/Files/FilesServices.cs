using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Services.ApiDatabase;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Season.DesarrolloBVF.Domain.Responses;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Data;
using System.Net.Http.Headers;

namespace DesarrolloVariedadesApp.Services.Files
{
    public class FilesServices : IFilesServices
    {
        private readonly IApiDatabaseService apiDatabaseService;

        public FilesServices(IApiDatabaseService apiDatabaseService)
        {
            this.apiDatabaseService = apiDatabaseService;
            if (!Directory.Exists(AppSettings.ImagesUrl))
            {
                Directory.CreateDirectory(AppSettings.ImagesUrl);
            }
        }
        public async Task<string> UploadImagen(string url, string nombre)
        {
            const long unMegabyteEnBytes = 1 * 1024 * 1024; // 1 MB en bytes

            // Obtener el tamaño del archivo
            //var nombreArchivo = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.jpeg";

            string nombreArchivo = nombre;
            if (nombreArchivo.IsNullOrEmpty())
                nombreArchivo = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.jpeg";

            var rutaArchivo = Path.Combine(AppSettings.ImagesUrl, nombreArchivo);

            var fileInfo = new FileInfo(url);
            
            using (var stream = File.OpenRead(url))
            {
                if (fileInfo.Length >= unMegabyteEnBytes)
                {
                    using (var image = SixLabors.ImageSharp.Image.Load(stream))
                    {
                        // Redimensionar la imagen manteniendo la relación de aspecto
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new SixLabors.ImageSharp.Size(1024, 768),
                            Mode = SixLabors.ImageSharp.Processing.ResizeMode.Max
                        }));

                        // Comprimir la imagen con la calidad especificada
                        var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder
                        {
                            Quality = 100
                        };
                        var outputStream = new System.IO.MemoryStream();
                        image.Save(rutaArchivo, encoder);
                    }
                }
                else
                {
                    using (var outputStream = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write))
                    {
                        await stream.CopyToAsync(outputStream);
                    }
                }
            }
            
#if ANDROID
            if (File.Exists(url))
            {
                // Elimina el archivo
                File.Delete(url);
            }
#endif
            return nombreArchivo;
        }

        public async Task DescargarImagenesDelServidor(IEnumerable<string> urls)
        {
            string[] archivos = GetFiles(AppSettings.ImagesUrl);
            archivos = archivos.Select(x=> Path.GetFileName(x)).ToArray();
            var command = urls.Where(x => !archivos.Contains(Path.GetFileName(x))).ToList();
            if (command.Count <= 0)
            { return; }
            GetImagesQueryResponse response = await this.apiDatabaseService.PostRequest<GetImagesQueryResponse>("DesarrolloVariedades/GetImages", JsonConvert.SerializeObject(command));
            var i = 0;
            foreach (var image in response.Images)
            {
                File.WriteAllBytes(Path.Combine(AppSettings.ImagesUrl, $"{Path.GetFileName(command.ToArray()[i])}"), image);
                i++;
            }
        }
        public async Task SubirImagenAlServidor<T>(T imagen)
        {
            string archivo = string.Empty;
            string nombre = string.Empty;
            string apiPath = string.Empty;
            if (imagen is DesVarImagenesApp desVarImagenes)
            {
                archivo = desVarImagenes.FullPath;
                nombre = Path.GetFileName(desVarImagenes.UrlImagen);
                if (desVarImagenes.TipoAccion == "A")
                    apiPath = "SubirDesVarImagen";
                else if (desVarImagenes.TipoAccion == "M")
                    apiPath = "UpdateImagen";
            }
            else if (imagen is DesVarMovimientosApp desVarMovimientos)
            {
                archivo = desVarMovimientos.FullPath;
                nombre = Path.GetFileName(desVarMovimientos.UrlImagen);
                if (desVarMovimientos.TipoAccion == "A")
                    apiPath = "SubirDesVarMovimientos";
            }
            else
                return;
            using (var formData = new MultipartFormDataContent())
            {
                if (File.Exists(archivo))
                {
                    var stream = File.OpenRead(archivo);
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    formData.Add(fileContent, "File", nombre);
                    var properties = typeof(T).GetProperties();
                    foreach (var property in properties)
                    {
                        var value = property.GetValue(imagen)?.ToString() ?? "";
                        formData.Add(new StringContent(value), property.Name);
                    }
                    await apiDatabaseService.PostRequest($"DesarrolloVariedades/{apiPath}", formData);
                }
            }
        }

        public string GetUrlImage(string imageName)
        {
            return Path.Combine(AppSettings.ImagesUrl, imageName);
        }
        private string[] GetFiles(string url)
        {
            string[] archivos;
            if (Directory.Exists(url))
            {
                // Obtener todos los archivos del directorio
                return Directory.GetFiles(url);
            }
            return [];
        }

    }
}
