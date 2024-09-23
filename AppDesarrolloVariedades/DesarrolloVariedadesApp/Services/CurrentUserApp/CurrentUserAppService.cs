using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DesarrolloVariedadesApp.Services.CurrentUserApp
{
    public class CurrentUserAppService : ICurrentUserAppService
    {
        public string DbApiToken { get => Preferences.Get(nameof(DbApiToken), ""); set => Preferences.Set(nameof(DbApiToken), value); }

        public bool IsAutenticated { get => ValidarToken(); }

        public int UserId
        {
            get
            {
                int.TryParse(GetClaim(nameof(UserId)), out var userId);
                return userId;
            }
        }

        public IEnumerable<string> Roles
        {
            get
            {
                var roles = GetClaim("Roles");
                if (roles != "")
                    return JsonConvert.DeserializeObject<IEnumerable<string>>(roles);

                return [];
            }
        }

        public string UserName { get => GetClaim("UserName"); }


        private string GetClaim(string claimName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                if (tokenHandler.ReadToken(DbApiToken) is not JwtSecurityToken jwtToken || jwtToken.Claims == null)
                {
                    // Token no válido o no contiene claims
                    return "";
                }

                var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimName);
                return claim?.Value;
            }
            catch
            {
                return "";
            }
        }
        private bool ValidarToken()
        {
            var keyEncoding = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.ApiKey));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidAudience = "JWTServiceDesarrolloVariedadesBVFClient",
                ValidIssuer = AppSettings.Issuer,
                IssuerSigningKey = keyEncoding,
            };

            try
            {
                // Intenta validar el token
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(DbApiToken, validationParameters, out var securityToken);
                // Si no hay excepción, el token es válido
                return true;
            }
            catch
            {
                // Si hay una excepción, el token no es válido
                return false;
            }
        }

    }
}
