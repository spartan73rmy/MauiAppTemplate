using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class DesVarUbicacionRelativaConfiguration : IEntityTypeConfiguration<DesVarUbicacionRelativaApp>
    {
        public void Configure(EntityTypeBuilder<DesVarUbicacionRelativaApp> builder)
        {
            builder.Property(x => x.IdUbicacion).ValueGeneratedOnAdd();
        }
    }
}
