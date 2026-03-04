using ProyectoG3.Models;

namespace ProyectoG3.Application.Interfaces
{
    internal interface IBitacoraService
    {   Task<IEnumerable<BitacoraEvento>> GetAllAsync();
        Task<BitacoraEvento> CreateAsync(BitacoraEvento evento);
    }
}
