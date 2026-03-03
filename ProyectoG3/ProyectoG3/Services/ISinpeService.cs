using ProyectoG3.Models;

namespace ProyectoG3.Services
{
    public interface ISinpeService
    {
        Task<IEnumerable<Sinpe>> ObtenerSinpePorTelefono(string telefono);
    }
}