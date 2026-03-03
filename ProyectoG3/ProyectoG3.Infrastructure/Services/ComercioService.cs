using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Infrastructure.Persistence;

namespace ProyectoG3.Infrastructure.Services
{
    public class ComercioService : IComercioService
    {
        private readonly AppDbContext _context;

        public ComercioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ComercioListDto>> GetAllAsync()
        {
            return await _context.Comercios
                .AsNoTracking()
                .OrderByDescending(c => c.IdComercio)
                .Select(c => new ComercioListDto
                {
                    IdComercio = c.IdComercio,
                    Identificacion = c.Identificacion,
                    TipoIdentificacion = c.TipoIdentificacion,
                    Nombre = c.Nombre,
                    TipoDeComercio = c.TipoDeComercio,
                    Telefono = c.Telefono,
                    CorreoElectronico = c.CorreoElectronico,
                    Estado = c.Estado
                })
                .ToListAsync();
        }

        public async Task<ComercioDetailsDto?> GetByIdAsync(int idComercio)
        {
            return await _context.Comercios
                .AsNoTracking()
                .Where(c => c.IdComercio == idComercio)
                .Select(c => new ComercioDetailsDto
                {
                    IdComercio = c.IdComercio,
                    Identificacion = c.Identificacion,
                    TipoIdentificacion = c.TipoIdentificacion,
                    Nombre = c.Nombre,
                    TipoDeComercio = c.TipoDeComercio,
                    Telefono = c.Telefono,
                    CorreoElectronico = c.CorreoElectronico,
                    Direccion = c.Direccion,
                    FechaDeRegistro = c.FechaDeRegistro,
                    FechaDeModificacion = c.FechaDeModificacion,
                    Estado = c.Estado
                })
                .FirstOrDefaultAsync();
        }

        public async Task<(bool Ok, string Message)> CreateAsync(ComercioCreateDto dto)
        {
            try
            {
                bool existe = await _context.Comercios
                    .AnyAsync(c => c.Identificacion == dto.Identificacion);

                if (existe)
                    return (false, "Ya existe un comercio con esa identificación.");

                var entity = new Comercio
                {
                    Identificacion = dto.Identificacion.Trim(),
                    TipoIdentificacion = dto.TipoIdentificacion,
                    Nombre = dto.Nombre.Trim(),
                    TipoDeComercio = dto.TipoDeComercio,
                    Telefono = dto.Telefono.Trim(),
                    CorreoElectronico = dto.CorreoElectronico.Trim(),
                    Direccion = dto.Direccion.Trim(),
                    FechaDeRegistro = DateTime.Now,
                    FechaDeModificacion = null,
                    Estado = true
                };

                _context.Comercios.Add(entity);
                await _context.SaveChangesAsync();

                return (true, "Comercio registrado correctamente.");
            }
            catch
            {
                return (false, "Ocurrió un error inesperado al registrar el comercio.");
            }
        }

        public async Task<(bool Ok, string Message)> UpdateAsync(ComercioUpdateDto dto)
        {
            try
            {
                var entity = await _context.Comercios
                    .FirstOrDefaultAsync(c => c.IdComercio == dto.IdComercio);

                if (entity == null)
                    return (false, "El comercio no existe.");

                entity.Nombre = dto.Nombre.Trim();
                entity.TipoDeComercio = dto.TipoDeComercio;
                entity.Telefono = dto.Telefono.Trim();
                entity.CorreoElectronico = dto.CorreoElectronico.Trim();
                entity.Direccion = dto.Direccion.Trim();
                entity.Estado = dto.Estado;
                entity.FechaDeModificacion = DateTime.Now;

                await _context.SaveChangesAsync();

                return (true, "Comercio actualizado correctamente.");
            }
            catch
            {
                return (false, "Ocurrió un error inesperado al actualizar el comercio.");
            }
        }
    }
}