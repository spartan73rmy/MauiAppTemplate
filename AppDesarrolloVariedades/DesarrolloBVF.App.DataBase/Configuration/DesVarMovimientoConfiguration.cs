using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class DesVarMovimientoConfiguration : IEntityTypeConfiguration<DesVarMovimientosApp>
    {
        public void Configure(EntityTypeBuilder<DesVarMovimientosApp> builder)
        {
            builder.Property(x => x.IdApp).ValueGeneratedOnAdd();
        }
    }
}
