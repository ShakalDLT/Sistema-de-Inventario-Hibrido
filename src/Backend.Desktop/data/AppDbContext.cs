using Microsoft.EntityFrameworkCore;
using Backend.Desktop.Models;

namespace Backend.Desktop.Data
{
    public class AppDbContext : DbContext
    {
        // Tablas principales del sistema
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=inventario.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Opcional: Configuración de relaciones para asegurar integridad
            modelBuilder.Entity<Venta>()
                .HasMany(v => v.Detalles)
                .WithOne()
                .HasForeignKey("VentaId");
        }
    }
}