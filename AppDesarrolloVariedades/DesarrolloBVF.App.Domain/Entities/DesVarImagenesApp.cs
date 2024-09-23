using DesarrolloBVF.App.Domain.Entities.Common;
using Season.DesarrolloBVF.Domain.Entities.VariantEntities;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class DesVarImagenesApp : DesVarImagenes, MobileEntityBase
    {
        [Key]
        public int IdApp { get; set; }
        public string? TipoAccion { get; set; }
        public bool Descargada { get; set; }
        public string FullPath { get; set; } = "";
        [JsonIgnore]
        public HistDesarrolloVariedadesApp Desarrollo { get; set; }

        public DesVarImagenesApp()
        { }

        public DesVarImagenesApp(DesVarImagenes desVarImagenes, string imagesUrl)
        {
            IdImagen = desVarImagenes.IdImagen;
            IdCultivo = desVarImagenes.IdCultivo;
            IdVariedad = desVarImagenes.IdVariedad;
            IdDesarrollo = desVarImagenes.IdDesarrollo;
            UrlImagen = desVarImagenes.UrlImagen;
            Comentario = desVarImagenes.Comentario;
            Activo = desVarImagenes.Activo;
            FechaRegistro = desVarImagenes.FechaRegistro;
            Tipo = desVarImagenes.Tipo;
            FullPath = desVarImagenes.UrlImagen == null ? "" : Path.Combine(imagesUrl, Path.GetFileName(desVarImagenes.UrlImagen));
        }
    }
}