using ProyectoG3.Application.DTOs;

namespace ProyectoG3.Application.Interfaces
{
    public interface IConfiguracionComercioService
    {
        Task<List<ConfiguracionComercioListDto>> GetAllByComercioAsync(int idComercio);
        Task<ConfiguracionComercioListDto?> GetByIdAsync(int idConfiguracion);
        Task<(bool Ok, string Message)> CreateAsync(ConfiguracionComercioCreateDto dto);
        Task<(bool Ok, string Message)> UpdateAsync(ConfiguracionComercioUpdateDto dto);
    }
}