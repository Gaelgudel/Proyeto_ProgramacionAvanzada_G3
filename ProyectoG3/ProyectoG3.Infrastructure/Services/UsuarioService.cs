using ProyectoG3.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Infrastructure.Persistence;

namespace ProyectoG3.Infrastructure.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR 
        public async Task<List<UsuarioDto>> ListarAsync()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombres = u.Nombres,
                    PrimerApellido = u.PrimerApellido,
                    SegundoApellido = u.SegundoApellido,
                    Identificacion = u.Identificacion,
                    CorreoElectronico = u.CorreoElectronico,
                    FechaDeRegistro = u.FechaDeRegistro,
                    FechaDeModificacion = u.FechaDeModificacion,
                    Estado = u.Estado
                }).ToListAsync();
        }

        // REGISTRAR 
        public async Task<bool> RegistrarAsync(UsuarioCreateDto dto)
        {
            var existe = await _context.Usuarios
                .AnyAsync(u => u.Identificacion == dto.Identificacion);

            if (existe)
                return false;

            var usuario = new Usuario
            {
                IdComercio = dto.IdComercio,
                Nombres = dto.Nombres,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                Identificacion = dto.Identificacion,
                CorreoElectronico = dto.CorreoElectronico,
                FechaDeRegistro = DateTime.Now,
                Estado = true
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        // OBTENER POR ID
        public async Task<UsuarioDto?> ObtenerPorIdAsync(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);

            if (u == null) return null;

            return new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                Nombres = u.Nombres,
                PrimerApellido = u.PrimerApellido,
                SegundoApellido = u.SegundoApellido,
                Identificacion = u.Identificacion,
                CorreoElectronico = u.CorreoElectronico,
                Estado = u.Estado
            };
        }

        // EDITAR 
        public async Task<bool> EditarAsync(int id, UsuarioUpdateDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return false;

            var existe = await _context.Usuarios
                .AnyAsync(u => u.Identificacion == dto.Identificacion && u.IdUsuario != id);

            if (existe)
                return false;

            usuario.Nombres = dto.Nombres;
            usuario.PrimerApellido = dto.PrimerApellido;
            usuario.SegundoApellido = dto.SegundoApellido;
            usuario.Identificacion = dto.Identificacion;
            usuario.CorreoElectronico = dto.CorreoElectronico;
            usuario.Estado = dto.Estado;
            usuario.FechaDeModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}