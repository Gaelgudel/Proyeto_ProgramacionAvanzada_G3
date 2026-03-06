using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ProyectoG3.Infrastructure.Services
{
    public class SinpeService : ISinpeService
    {
        private readonly AppDbContext _context;

        public SinpeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegistrarAsync(SinpeCreateDto dto)
        {
            var sinpe = new Sinpe
            {
                TelefonoOrigen = dto.TelefonoOrigen,
                NombreOrigen = dto.NombreOrigen,
                TelefonoDestinatario = dto.TelefonoDestinatario,
                NombreDestinatario = dto.NombreDestinatario,
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                FechaDeRegistro = DateTime.Now,
                Estado = false
            };

            _context.Sinpes.Add(sinpe);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Sinpe>> ObtenerSinpePorTelefono(string telefono)
        {
            return await _context.Sinpes
                .Where(s => s.TelefonoOrigen == telefono || s.TelefonoDestinatario == telefono)
                .ToListAsync();
        }
    }
}