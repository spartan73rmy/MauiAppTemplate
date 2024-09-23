using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesarrolloBVF.App.DataBase.Configuration
{
    public class CatTiposProdConfiguration : IEntityTypeConfiguration<CatTiposProdApp>
    {
        public void Configure(EntityTypeBuilder<CatTiposProdApp> builder)
        {
        }
    }
}
