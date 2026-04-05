using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Infrastructure.Persistence;

namespace ProyectoG3.Infrastructure.Services
{
    public class ConfiguracionComercioService : IConfiguracionComercioService
    {
        private readonly AppDbContext _context;
        private readonly IBitacoraService _bitacora;

        public ConfiguracionComercioService(AppDbContext context, IBitacoraService bitacora)
        {
            _context = context;
            _bitacora = bitacora;
        }

        public async Task<List<ConfiguracionComercioListDto>> GetAllByComercioAsync(int idComercio)
        {
            return await _context.ConfiguracionesComercio
                .AsNoTracking()
                .Where(x => x.IdComercio == idComercio)
                .Include(x => x.Comercio)
                .OrderByDescending(x => x.IdConfiguracion)
                .Select(x => new ConfiguracionComercioListDto
                {
                    IdConfiguracion = x.IdConfiguracion,
                    IdComercio = x.IdComercio,
                    NombreComercio = x.Comercio!.Nombre,
                    TipoConfiguracion = x.TipoConfiguracion,
                    TipoConfiguracionTexto = x.TipoConfiguracion == 1 ? "Plataforma"
                                           : x.TipoConfiguracion == 2 ? "Externa"
                                           : "Ambas",
                    Comision = x.Comision,
                    FechaDeRegistro = x.FechaDeRegistro,
                    FechaDeModificacion = x.FechaDeModificacion,
                    Estado = x.Estado
                })
                .ToListAsync();
        }

        public async Task<ConfiguracionComercioListDto?> GetByIdAsync(int idConfiguracion)
        {
            return await _context.ConfiguracionesComercio
                .AsNoTracking()
                .Where(x => x.IdConfiguracion == idConfiguracion)
                .Include(x => x.Comercio)
                .Select(x => new ConfiguracionComercioListDto
                {
                    IdConfiguracion = x.IdConfiguracion,
                    IdComercio = x.IdComercio,
                    NombreComercio = x.Comercio!.Nombre,
                    TipoConfiguracion = x.TipoConfiguracion,
                    TipoConfiguracionTexto = x.TipoConfiguracion == 1 ? "Plataforma"
                                           : x.TipoConfiguracion == 2 ? "Externa"
                                           : "Ambas",
                    Comision = x.Comision,
                    FechaDeRegistro = x.FechaDeRegistro,
                    FechaDeModificacion = x.FechaDeModificacion,
                    Estado = x.Estado
                })
                .FirstOrDefaultAsync();
        }

        public async Task<(bool Ok, string Message)> CreateAsync(ConfiguracionComercioCreateDto dto)
        {
            try
            {
                // Validar que el comercio no tenga ya una configuración
                bool yaExiste = await _context.ConfiguracionesComercio
                    .AnyAsync(x => x.IdComercio == dto.IdComercio);

                if (yaExiste)
                    return (false, "Este comercio ya tiene una configuración registrada.");

                var entity = new ConfiguracionComercio
                {
                    IdComercio = dto.IdComercio,
                    TipoConfiguracion = dto.TipoConfiguracion,
                    Comision = dto.Comision,
                    FechaDeRegistro = DateTime.Now,
                    FechaDeModificacion = null,
                    Estado = true
                };

                _context.ConfiguracionesComercio.Add(entity);
                await _context.SaveChangesAsync();

                await _bitacora.RegistrarEventoAsync(
                    "ConfiguracionComercio",
                    "Registrar",
                    "La configuración del comercio fue creada con éxito.",
                    null,
                    entity
                );

                return (true, "Configuración registrada correctamente.");
            }
            catch (Exception ex)
            {
                await _bitacora.RegistrarErrorAsync(
                    "ConfiguracionComercio",
                    ex.Message,
                    ex.StackTrace ?? "Sin stack trace"
                );
                return (false, "Ocurrió un error inesperado al registrar la configuración.");
            }
        }

        public async Task<(bool Ok, string Message)> UpdateAsync(ConfiguracionComercioUpdateDto dto)
        {
            try
            {
                var entity = await _context.ConfiguracionesComercio
                    .FirstOrDefaultAsync(x => x.IdConfiguracion == dto.IdConfiguracion);

                if (entity is null)
                    return (false, "No se encontró la configuración.");

                var datosAnteriores = new
                {
                    entity.TipoConfiguracion,
                    entity.Comision,
                    entity.Estado
                };

                entity.TipoConfiguracion = dto.TipoConfiguracion;
                entity.Comision = dto.Comision;
                entity.Estado = dto.Estado;
                entity.FechaDeModificacion = DateTime.Now;

                await _context.SaveChangesAsync();

                await _bitacora.RegistrarEventoAsync(
                    "ConfiguracionComercio",
                    "Editar",
                    "La configuración del comercio fue actualizada.",
                    datosAnteriores,
                    entity
                );

                return (true, "Configuración actualizada correctamente.");
            }
            catch (Exception ex)
            {
                await _bitacora.RegistrarErrorAsync(
                    "ConfiguracionComercio",
                    ex.Message,
                    ex.StackTrace ?? "Sin stack trace"
                );
                return (false, "Ocurrió un error inesperado al actualizar la configuración.");
            }
        }
    }
}