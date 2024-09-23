namespace DesarrolloVariedadesApp.Services.CurrentUserApp
{
    public interface ICurrentUserAppService
    {
        //string Token { get; set; }
        string DbApiToken { get; set; }
        bool IsAutenticated { get; }
        int UserId { get; }
        IEnumerable<string> Roles { get; }

        string UserName { get; }
    }
}