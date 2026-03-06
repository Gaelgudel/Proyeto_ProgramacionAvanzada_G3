using ProyectoG3.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoG3.Infrastructure.Persistence;
using ProyectoG3.Models;
using System.Text.Json;

namespace ProyectoG3.Infrastructure.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly AppDbContext _context;
        public BitacoraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BitacoraEvento>> ListarBitacoraAsync()
        {
            return await _context.BitacoraEventos
                .OrderByDescending(x => x.FechaDeEvento)
                .ToListAsync();
        }

        public async Task RegistrarEventoAsync(string tabla, string tipo, string descripcion, object? anterior = null, object? posterior = null)
        {
            var evento = new BitacoraEvento
            {
                TablaDeEvento = tabla,
                TipoDeEvento = tipo,
                FechaDeEvento = DateTime.Now,
                DescripcionDeEvento = descripcion,
                StackTrace = "N/A",
                DatosAnteriores = anterior != null ? JsonSerializer.Serialize(anterior) : null,
                DatosPosteriores = posterior != null ? JsonSerializer.Serialize(posterior) : null
            };

            _context.BitacoraEventos.Add(evento);
            await _context.SaveChangesAsync();
        }

        public async Task RegistrarErrorAsync(string tabla, string mensaje, string stackTrace)
        {
            var evento = new BitacoraEvento
            {
                TablaDeEvento = tabla,
                TipoDeEvento = "Error",
                FechaDeEvento = DateTime.Now,
                DescripcionDeEvento = mensaje,
                StackTrace = stackTrace ?? "No trace",
                DatosAnteriores = null,
                DatosPosteriores = null
            };

            _context.BitacoraEventos.Add(evento);
            await _context.SaveChangesAsync();
        }
    }
}
