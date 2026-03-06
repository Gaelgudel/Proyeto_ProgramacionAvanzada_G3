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


    }
}