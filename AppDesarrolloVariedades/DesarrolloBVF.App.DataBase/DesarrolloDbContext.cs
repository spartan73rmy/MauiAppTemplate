using DesarrolloBVF.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Season.DesarrolloBVF.Domain.Entities;

namespace DesarrolloBVF.App.DataBase
{
    public class DesarrolloDbContext : DbContext
    {
        private readonly string path;

        public DesarrolloDbContext(string path)
        {
            this.path = path;
        }
        public DesarrolloDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UnidadesAux> UnidadesPresentacion { get; set; }
        public DbSet<PresentacionesAux> TiposPresentacion { get; set; }
        public DbSet<ProcPresentacionEmpApp> ProcPresentacionEmps { get; set; }

        public DbSet<CatTiposProdApp> CatCultivos { get; set; }
        public DbSet<CatProductosApp> CatVariedades { get; set; }
        public DbSet<ProcCatTipoPlanta> CatTipoPlanta { get; set; }
        public DbSet<ProcCatProductosApp> ProcCatProductos { get; set; }
        public DbSet<ProcCatProductosSegmentos> ProcCatProductosSegmentos { get; set; }
        public DbSet<CatCentrosCostos> CatCentrosCostos { get; set; }
        public DbSet<HistDesarrolloVariedadesApp> HistDesarrolloVariedades { get; set; }

        public DbSet<DesVarCatSitios> DesVarCatSitios { get; set; }
        public DbSet<DesVarTipos> DesVarTipos { get; set; }
        public DbSet<DesVarUbicacionRelativaApp> DesVarUbicacionRelativas { get; set; }

        public DbSet<DesVarImagenesApp> DesVarImagenes { get; set; }

        public DbSet<DesVarMovimientosTiposApp> DesVarMovimientosTipos { get; set; }
        public DbSet<DesVarMovimientosApp> DesVarMovimientos { get; set; }

        public DbSet<LogModel> LogModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.Entity<LogModel>().HasNoKey();
        }
    }
}
