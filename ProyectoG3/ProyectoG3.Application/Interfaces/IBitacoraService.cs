using ProyectoG3.Models;

namespace ProyectoG3.Application.Interfaces
{
    public interface IBitacoraService
    {   Task<IEnumerable<BitacoraEvento>> ListarBitacoraAsync();
        Task RegistrarEventoAsync(string tabla, string tipo, string descripcion, object? anterior = null, object? posterior = null);

        Task RegistrarErrorAsync(string tabla, string mensaje, string stackTrace);
    }
}
