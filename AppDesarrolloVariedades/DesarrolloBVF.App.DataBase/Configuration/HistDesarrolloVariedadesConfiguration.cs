using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class HistDesarrolloVariedadesConfiguration : IEntityTypeConfiguration<HistDesarrolloVariedadesApp>
    {
        public void Configure(EntityTypeBuilder<HistDesarrolloVariedadesApp> builder)
        {
            builder.HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.IdProducto);
            builder.HasOne(x => x.Presentacion).WithMany().HasForeignKey(x => x.IdPresentacion);
            builder.HasOne(x => x.ReferenciaPadre).WithMany().HasForeignKey(x => x.IdReferenciaPadre);
            builder.HasMany(x => x.Ubicaciones).WithOne(x => x.Desarrollo).HasForeignKey(x => x.IdDesarrollo);
            builder.HasOne(x => x.TipoDesarrollo).WithMany().HasForeignKey(x => x.IdTipoDesarrollo);
            builder.Navigation(x => x.TipoDesarrollo).AutoInclude();
        }
    }
}
