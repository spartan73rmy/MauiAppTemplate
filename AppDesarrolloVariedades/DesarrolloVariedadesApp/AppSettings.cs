namespace DesarrolloVariedadesApp
{
    public class AppSettings
    {
        public static string ApiKey => "LYzSKEhn1HFFYmJdinuuMS7QGoEM5Arf";
        public static string Issuer => "JWTAuthenticationDesarrolloVariedadesBVFProd";
        public static string ApiDatabaseUri => "https://giddingsfruit.mx/DesarrolloVariedadesAPI/Api/";
        //public static string ApiDatabaseUri => "https://giddingsfruit.mx/DesarrolloVariedadesAPIDemo/Api/";
        //public static string ApiDatabaseUri => "http://localhost:5203/Api/";//"http://192.168.80.179:42141/Api/";
        public static string ImagesUrl => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "DesarrolloVariedades", "Imagenes");
        public static string AppVersion => "1.0";
        public static int VersionNumber => 1;

        public static AppSettings Intance { get; } = new AppSettings();
    }
}
