using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Season.DesarrolloBVF.Domain.Entities;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class CatCentrosCostosConfiguration : IEntityTypeConfiguration<CatCentrosCostos>
    {
        public void Configure(EntityTypeBuilder<CatCentrosCostos> builder)
        {
            builder.HasKey(x => new { x.Codigo, x.SubCodigo });
        }
    }
}
