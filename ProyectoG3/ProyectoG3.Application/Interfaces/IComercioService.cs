using ProyectoG3.Application.DTOs;

namespace ProyectoG3.Application.Interfaces
{
    public interface IComercioService
    {
        Task<List<ComercioListDto>> GetAllAsync();
        Task<ComercioDetailsDto?> GetByIdAsync(int idComercio);
        Task<(bool Ok, string Message)> CreateAsync(ComercioCreateDto dto);
        Task<(bool Ok, string Message)> UpdateAsync(ComercioUpdateDto dto);
    }
}