using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class ProcCatProductosSegmentosConfiguration : IEntityTypeConfiguration<ProcCatProductosSegmentosApp>
    {
        public void Configure(EntityTypeBuilder<ProcCatProductosSegmentosApp> builder)
        {
            builder.HasOne(x => x.CentroCosto).WithMany().HasForeignKey(x => new { x.Codigo, x.SubCodigo}).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
