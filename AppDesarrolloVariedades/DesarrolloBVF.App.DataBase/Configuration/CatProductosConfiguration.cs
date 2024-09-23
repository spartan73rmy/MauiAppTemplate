using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class CatProductosConfiguration : IEntityTypeConfiguration<CatProductosApp>
    {
        public void Configure(EntityTypeBuilder<CatProductosApp> builder)
        {
            builder.HasKey(x => new { x.Tipo, x.Producto });
            builder.HasOne(x => x.Cultivo).WithMany(x => x.Variedades).HasForeignKey(x => x.Tipo);            
        }
    }
}
