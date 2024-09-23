using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DesarrolloBVF.App.DataBase
{
    public class DesarrolloDbContextFactory : IDesignTimeDbContextFactory<DesarrolloDbContext>
    {
        public DesarrolloDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DesarrolloDbContext>();
            optionsBuilder.UseSqlite("Data Source=DbDesarrolloVariedades.db");

            return new DesarrolloDbContext(optionsBuilder.Options);
        }
    }
}
