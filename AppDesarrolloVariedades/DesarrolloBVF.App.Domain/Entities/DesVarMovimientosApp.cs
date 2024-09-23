using DesarrolloBVF.App.Domain.Entities.Common;
using Season.DesarrolloBVF.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DesarrolloBVF.App.Domain.Entities
{
    public class DesVarMovimientosApp : DesVarMovimientos, MobileEntityBase
    {
        [Key]
        public int IdApp { get; set; }
        public string? TipoAccion { get; set; }
        public bool Descargada { get; set; }
        public string FullPath { get; set; } = "";
        public DesVarMovimientosApp()
        {}
        public DesVarMovimientosApp(DesVarMovimientos movimiento, string imagesUrl)
        {
            IdMovimiento = movimiento.IdMovimiento;
            IdDesarrollo = movimiento.IdDesarrollo;
            UrlImagen = movimiento.UrlImagen;
            Comentario = movimiento.Comentario;
            Accion = movimiento.Accion;
            FechaRegistro = movimiento.FechaRegistro;
            IdTipo = movimiento.IdTipo;
            FullPath = movimiento.UrlImagen == null ? "" : Path.Combine(imagesUrl, Path.GetFileName(movimiento.UrlImagen));
        }
    }
}