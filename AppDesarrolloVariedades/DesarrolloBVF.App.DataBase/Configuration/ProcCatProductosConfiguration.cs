using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class ProcCatProductosConfiguration : IEntityTypeConfiguration<ProcCatProductosApp>
    {
        public void Configure(EntityTypeBuilder<ProcCatProductosApp> builder)
        {
            builder.HasOne(x => x.Variedad).WithMany(x => x.Produtos).HasForeignKey(x => new { x.Tipo, x.Producto});
            builder.HasOne(x => x.PlantaTipo).WithMany().HasForeignKey(x => x.TipoPlanta).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Segmento).WithOne().HasForeignKey<ProcCatProductosSegmentosApp>(x => x.CodProducto).OnDelete(DeleteBehavior.SetNull);
            builder.Navigation(x => x.PlantaTipo).AutoInclude();
            builder.Navigation(x => x.Segmento).AutoInclude();
        }
    }
}
