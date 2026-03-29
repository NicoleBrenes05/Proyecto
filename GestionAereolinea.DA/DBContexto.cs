using Microsoft.EntityFrameworkCore;

namespace GestionAereolinea.DA
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        {
        }

        public DbSet<Model.Aerolinea> Aerolineas { get; set; }
        public DbSet<Model.Avion> Aviones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.Avion>()
                .HasOne(a => a.Aerolinea)
                .WithMany(a => a.Aviones)
                .HasForeignKey(a => a.AerolineaId);
        }
    }
}
