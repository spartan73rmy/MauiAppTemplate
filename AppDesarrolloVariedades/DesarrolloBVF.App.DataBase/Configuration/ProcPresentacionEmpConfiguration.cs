using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class ProcPresentacionEmpConfiguration : IEntityTypeConfiguration<ProcPresentacionEmpApp>
    {
        public void Configure(EntityTypeBuilder<ProcPresentacionEmpApp> builder)
        {
            builder.HasOne(x => x.TipoPresetacion).WithMany().HasForeignKey(x => x.IdPresentacion).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Unidad).WithMany().HasForeignKey(x => x.IdUnidad).OnDelete(DeleteBehavior.SetNull);
            builder.Navigation(x => x.TipoPresetacion).AutoInclude();
            builder.Navigation(x => x.Unidad).AutoInclude();
        }
    }
}
