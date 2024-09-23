using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Authentication;

namespace DesarrolloVariedadesApp.Helpers
{
    public class RequestHandler
    {
        public static async Task<(bool success, string titulo, string mensaje)> TaskRequestHandler(Func<Task<string>> task)
        {
            try
            {
                var mensaje = await task();
                return (true, "", mensaje);
            }
            catch (AuthenticationException ex)
            {
                //Application.Current?.MainPage?.DisplayAlert("Error de inicio de sesión", ex.Message, "Aceptar");
                return (false, "Error de inicio de sesión", ex.Message);
            }
            catch (HttpRequestException ex)
            {
                //Application.Current?.MainPage?.DisplayAlert("Error de solicitud:500", "Por favor contacte a soporte explicando lo que intento hacer y que hay un problema en la API", "Aceptar");
                return (false, "Error de solicitud: 500", ex.Message);
            }
            catch (WebException ex)
            {
                //Application.Current?.MainPage?.DisplayAlert("Error de conexion", $"Problemas en la red: \n{ex.Message}", "Aceptar");
                return (false, "Error de conexion", $"Problemas en la red: \n{ex.Message}");
            }
            catch (OperationCanceledException ex)
            {
                //Application.Current?.MainPage?.DisplayAlert("Comprueba tu conexión", "Compruebe la estabilidad de su red, no fue posible conectarse con el servicio", "Aceptar");
                return (false, "Comprueba tu conexión", "Compruebe la estabilidad de su red, no fue posible conectarse con el servicio");
            }
            catch (DbUpdateException ex)
            {
                //Application.Current?.MainPage?.DisplayAlert("Error en la app", ex.Message, "Aceptar");
                return (false, "Error en la app", ex.Message);
            }
            catch (Exception ex)
            {
                //Application.Current?.MainPage?.DisplayAlert("Error desconocido", "Contacte a soporte", "Aceptar");
                return (false, "Error desconocido", $"Contacte a soporte, ERROR: {ex.Message}");
            }
        }

        public static async Task<bool> TaskRequestHandlerBasic(Func<Task> task)
        {
            try
            {
                await task();
                return true;
            }
            catch (AuthenticationException ex)
            {
                Application.Current?.MainPage?.DisplayAlert("Error de inicio de sesión", ex.Message, "Aceptar");
                return false;
            }
            catch (HttpRequestException ex)
            {
                Application.Current?.MainPage?.DisplayAlert("Error de solicitud:500", ex.Message, "Aceptar");
                return false;
            }
            catch (WebException ex)
            {
                Application.Current?.MainPage?.DisplayAlert("Error de conexion", $"Problemas en la red: \n{ex.Message}", "Aceptar");
                return false;
            }
            catch (OperationCanceledException ex)
            {
                Application.Current?.MainPage?.DisplayAlert("Comprueba tu conexión", "Compruebe la estabilidad de su red, no fue posible conectarse con el servicio", "Aceptar");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Application.Current?.MainPage?.DisplayAlert("Error en la app", ex.Message, "Aceptar");
                return false;
            }
            catch (Exception ex)
            {
                Application.Current?.MainPage?.DisplayAlert("Error desconocido", $"Contacte a soporte, ERROR: {ex.Message}", "Aceptar");
                return false;
            }
        }
    }
}
