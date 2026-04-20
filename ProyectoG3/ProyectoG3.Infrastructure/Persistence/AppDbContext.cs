using Microsoft.EntityFrameworkCore;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoG3.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Comercio> Comercios { get; set; }
        public DbSet<Sinpe> Sinpes { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<BitacoraEvento> BitacoraEventos { get; set; }

        public DbSet<ConfiguracionComercio> ConfiguracionesComercio { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<ReporteMensual> ReportesMensuales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BitacoraEvento>(entity =>
            {
                entity.Property(e => e.TablaDeEvento).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TipoDeEvento).HasMaxLength(20).IsRequired();
                entity.Property(e => e.DescripcionDeEvento).HasColumnType("longtext").IsRequired();
                entity.Property(e => e.StackTrace).HasColumnType("longtext").IsRequired();
                entity.Property(e => e.DatosAnteriores).HasColumnType("longtext");
                entity.Property(e => e.DatosPosteriores).HasColumnType("longtext");
            });
        }
    }
}