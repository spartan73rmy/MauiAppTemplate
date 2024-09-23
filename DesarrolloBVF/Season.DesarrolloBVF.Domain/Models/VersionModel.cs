namespace Season.DesarrolloBVF.Domain.Models
{
    public class VersionModel
    {
        public int VersionNumber { get; set; }
        public string AppVersion { get; set; }
        public string UpdateLink { get; set; }
        public bool Forzar { get; set; }
    }
}
