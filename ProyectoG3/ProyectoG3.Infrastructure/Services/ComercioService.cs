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
                .OrderByDescending(x => x.IdComercio)
                .Select(x => new ComercioListDto
                {
                    IdComercio = x.IdComercio,
                    Identificacion = x.Identificacion,
                    TipoIdentificacion = x.TipoIdentificacion,
                    Nombre = x.Nombre,
                    TipoDeComercio = x.TipoDeComercio,
                    Telefono = x.Telefono,
                    CorreoElectronico = x.CorreoElectronico,
                    Estado = x.Estado
                })
                .ToListAsync();
        }

        public async Task<ComercioDetailsDto?> GetByIdAsync(int idComercio)
        {
            return await _context.Comercios
                .AsNoTracking()
                .Where(x => x.IdComercio == idComercio)
                .Select(x => new ComercioDetailsDto
                {
                    IdComercio = x.IdComercio,
                    Identificacion = x.Identificacion,
                    TipoIdentificacion = x.TipoIdentificacion,
                    Nombre = x.Nombre,
                    TipoDeComercio = x.TipoDeComercio,
                    Telefono = x.Telefono,
                    CorreoElectronico = x.CorreoElectronico,
                    Direccion = x.Direccion,
                    FechaDeRegistro = x.FechaDeRegistro,
                    FechaDeModificacion = x.FechaDeModificacion,
                    Estado = x.Estado
                })
                .FirstOrDefaultAsync();
        }

        public async Task<(bool Ok, string Message)> CreateAsync(ComercioCreateDto dto)
        {
            try
            {
                var identificacion = (dto.Identificacion ?? string.Empty).Trim();
                var nombre = (dto.Nombre ?? string.Empty).Trim();
                var telefono = (dto.Telefono ?? string.Empty).Trim();
                var correo = (dto.CorreoElectronico ?? string.Empty).Trim();
                var direccion = (dto.Direccion ?? string.Empty).Trim();

                bool existe = await _context.Comercios.AnyAsync(x => x.Identificacion == identificacion);
                if (existe)
                    return (false, "Ya existe un comercio con esa identificación.");

                var entity = new Comercio
                {
                    Identificacion = identificacion,
                    TipoIdentificacion = dto.TipoIdentificacion,
                    Nombre = nombre,
                    TipoDeComercio = dto.TipoDeComercio,
                    Telefono = telefono,
                    CorreoElectronico = correo,
                    Direccion = direccion,
                    FechaDeRegistro = DateTime.Now,
                    FechaDeModificacion = null,
                    Estado = true
                };

                _context.Comercios.Add(entity);
                await _context.SaveChangesAsync();

                return (true, "Comercio registrado correctamente.");
            }
            catch (DbUpdateException)
            {
                // Por si el índice único se dispara (backup)
                return (false, "No se pudo registrar. La identificación ya existe.");
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
                var entity = await _context.Comercios.FirstOrDefaultAsync(x => x.IdComercio == dto.IdComercio);
                if (entity is null)
                    return (false, "No se encontró el comercio.");

                entity.Nombre = (dto.Nombre ?? string.Empty).Trim();
                entity.TipoDeComercio = dto.TipoDeComercio;
                entity.Telefono = (dto.Telefono ?? string.Empty).Trim();
                entity.CorreoElectronico = (dto.CorreoElectronico ?? string.Empty).Trim();
                entity.Direccion = (dto.Direccion ?? string.Empty).Trim();
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