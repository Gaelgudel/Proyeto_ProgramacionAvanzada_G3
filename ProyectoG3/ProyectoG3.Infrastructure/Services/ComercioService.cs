using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Domain.Enums;
using ProyectoG3.Infrastructure.Persistence;

namespace ProyectoG3.Infrastructure.Services
{
    public class ComercioService : IComercioService
    {
        private readonly AppDbContext _context;
        private readonly IBitacoraService _bitacora;

        public ComercioService(AppDbContext context, IBitacoraService bitacora)
        {
            _context = context;
            _bitacora = bitacora;
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
                    TipoIdentificacion = (TipoIdentificacion)x.TipoIdentificacion,
                    Nombre = x.Nombre,
                    TipoDeComercio = (TipoComercio)x.TipoDeComercio,
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
                    TipoIdentificacion = (TipoIdentificacion)x.TipoIdentificacion,
                    Nombre = x.Nombre,
                    TipoDeComercio = (TipoComercio)x.TipoDeComercio,
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

                var entity = new Comercio
                {
                    Identificacion = identificacion,
                    TipoIdentificacion = (int)dto.TipoIdentificacion,
                    Nombre = nombre,
                    TipoDeComercio = (int)dto.TipoDeComercio,
                    Telefono = telefono,
                    CorreoElectronico = correo,
                    Direccion = direccion,
                    FechaDeRegistro = DateTime.Now,
                    FechaDeModificacion = null,
                    Estado = true
                };

                _context.Comercios.Add(entity);
                await _context.SaveChangesAsync();

                await _bitacora.RegistrarEventoAsync(
                    "Comercios",
                    "Registrar",
                    "El comercio fue creado con éxito",
                    null,
                    entity
                );

                return (true, "Comercio registrado correctamente.");
            }
            catch (DbUpdateException)
            {
                // 🔥 AQUÍ SE MANEJA EL ÍNDICE ÚNICO
                return (false, "Ya existe un comercio con esa identificación.");
            }
            catch (Exception ex)
            {
                await _bitacora.RegistrarErrorAsync(
                    "Comercios",
                    ex.Message,
                    ex.StackTrace ?? "No tiene un stack trace"
                );

                return (false, "Ocurrió un error inesperado al registrar el comercio.");
            }
        }

        public async Task<(bool Ok, string Message)> UpdateAsync(ComercioUpdateDto dto)
        {
            try
            {
                var entity = await _context.Comercios
                    .FirstOrDefaultAsync(x => x.IdComercio == dto.IdComercio);

                if (entity is null)
                    return (false, "No se encontró el comercio.");

                var datosAnteriores = new
                {
                    entity.Nombre,
                    entity.TipoDeComercio,
                    entity.Telefono,
                    entity.CorreoElectronico,
                    entity.Direccion,
                    entity.Estado
                };

                entity.Nombre = (dto.Nombre ?? string.Empty).Trim();
                entity.TipoDeComercio = (int)dto.TipoDeComercio;
                entity.Telefono = (dto.Telefono ?? string.Empty).Trim();
                entity.CorreoElectronico = (dto.CorreoElectronico ?? string.Empty).Trim();
                entity.Direccion = (dto.Direccion ?? string.Empty).Trim();
                entity.Estado = dto.Estado;
                entity.FechaDeModificacion = DateTime.Now;

                await _context.SaveChangesAsync();

                await _bitacora.RegistrarEventoAsync(
                    "Comercios",
                    "Editar",
                    "Comercio actualizado",
                    datosAnteriores,
                    entity
                );

                return (true, "Comercio actualizado correctamente.");
            }
            catch (Exception ex)
            {
                await _bitacora.RegistrarErrorAsync(
                    "Comercios",
                    ex.Message,
                    ex.StackTrace ?? "no tiene stack trace"
                );

                return (false, "Ocurrió un error inesperado al actualizar el comercio.");
            }
        }
    }
}