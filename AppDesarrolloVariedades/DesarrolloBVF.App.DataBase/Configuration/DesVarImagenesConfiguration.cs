using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class DesVarImagenesConfiguration : IEntityTypeConfiguration<DesVarImagenesApp>
    {
        public void Configure(EntityTypeBuilder<DesVarImagenesApp> builder)
        {
            builder.Property(x => x.IdApp).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Desarrollo).WithMany(x => x.Imagenes).HasForeignKey(x => x.IdDesarrollo);
        }
    }
}
