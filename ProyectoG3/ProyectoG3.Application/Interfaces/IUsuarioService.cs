using ProyectoG3.Application.DTOs;

namespace ProyectoG3.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> ListarAsync();
        Task<bool> RegistrarAsync(UsuarioCreateDto dto);
        Task<UsuarioDto?> ObtenerPorIdAsync(int id);
        Task<bool> EditarAsync(int id, UsuarioUpdateDto dto);
    }
}