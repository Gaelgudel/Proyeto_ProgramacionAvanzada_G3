using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Infrastructure.Persistence;
using ProyectoG3.Models;

namespace ProyectoG3.Infrastructure.Services
{
    public class CajaService : ICajaService
    {
        private readonly AppDbContext _context;

        public CajaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Caja>> GetByComercioIdAsync(int idComercio)
        {
            return await _context.Cajas
                .Where(c => c.IdComercio == idComercio)
                .OrderByDescending(c => c.FechaDeRegistro)
                .ToListAsync();
        }

        public async Task<Caja?> GetByIdAsync(int id)
        {
            return await _context.Cajas
                .Include(c => c.Comercio)
                .FirstOrDefaultAsync(c => c.IdCaja == id);
        }

        public async Task<Caja> CreateAsync(Caja caja)
        {
            caja.FechaDeRegistro = DateTime.Now;
            caja.Estado = true;

            _context.Cajas.Add(caja);
            await _context.SaveChangesAsync();

            return caja;
        }

        public async Task UpdateAsync(Caja caja)
        {
            caja.FechaDeModificacion = DateTime.Now;
            _context.Cajas.Update(caja);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByTelefonoActivoAsync(string telefonoSINPE, int? excludeId = null)
        {
            var query = _context.Cajas
                .Where(c => c.TelefonoSINPE == telefonoSINPE && c.Estado == true);

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.IdCaja != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> ExistsByNombreAndComercioAsync(string nombre, int idComercio, int? excludeId = null)
        {
            var query = _context.Cajas
                .Where(c => c.Nombre == nombre && c.IdComercio == idComercio);

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.IdCaja != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
