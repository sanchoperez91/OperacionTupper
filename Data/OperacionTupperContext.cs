using Microsoft.EntityFrameworkCore;
using OperacionTupper2._0.Models; // con esto decimos que desde aqui "OperacionTupperContext.cs" tengamos acceso a las clases de la carpeta "Models"

namespace OperacionTupper2._0.Data
{
    // El DbContext es el puente entre tus modelos (clases) y la base de datos.
    public class OperacionTupperContext : DbContext
    {
        // Constructor: recibe las opciones de configuración (como la cadena de conexión)
        public OperacionTupperContext(DbContextOptions<OperacionTupperContext> options)
            : base(options)
        {
        }

        // Aquí  declaramos las tablas o modelos.cs:
        public DbSet<Plato> Platos { get; set; } = default!;
        public DbSet<Ingrediente> Ingredientes { get; set; } = default!;
        public DbSet<PlatoIngrediente> PlatoIngredientes { get; set; } = default!;
        public DbSet<Menu> Menus { get; set; } = default!;
        public DbSet<MenuDetalle> MenusDetalles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Índice único para evitar que se repita el mismo día/hora en un menú
            modelBuilder.Entity<MenuDetalle>()
                .HasIndex(md => new { md.IdMenu, md.DiaSemana, md.HoraComida })
                .IsUnique();
        }
    }
}

