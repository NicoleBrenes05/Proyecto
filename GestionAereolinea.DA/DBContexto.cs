using Microsoft.EntityFrameworkCore;

namespace GestionAereolinea.DA
{
    public class DBContexto : DbContext
    {
        // Clase que representa el contexto de la base de datos usando Entity Framework
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        {
        }
        // Representa la tabla de Aerolineas en la base de datos
        public DbSet<Model.Aerolinea> Aerolineas { get; set; }

        // Representa la tabla de Aviones en la base de datos
        public DbSet<Model.Avion> Aviones { get; set; }
        // Método que permite configurar las relaciones y reglas del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llama a la configuración base del modelo definida en DbContext
            base.OnModelCreating(modelBuilder);
            // Configura la relación entre la entidad Avion y Aerolinea
            modelBuilder.Entity<Model.Avion>()
                .HasOne(a => a.Aerolinea)// Cada avión pertenece a una sola aerolínea
                .WithMany(a => a.Aviones)// Una aerolínea puede tener muchos aviones
                .HasForeignKey(a => a.AerolineaId);// Se define AerolineaId como clave foránea
        }
    }
}
