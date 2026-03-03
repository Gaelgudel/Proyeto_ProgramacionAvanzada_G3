using ProyectoG3.Models;

namespace ProyectoG3.Repository
{
    public interface ICajaRepository
    {
        // Cambiamos los nombres para que coincidan con lo que pide el CajaService
        Task<IEnumerable<Caja>> ObtenerCajasPorComercio(int idComercio);
        Task<Caja> ObtenerPorId(int id);
        Task<bool> ValidarNombreEnComercio(string nombre, int idComercio);
        Task<bool> ValidarTelefonoGlobal(string telefono);
        Task<bool> Insertar(Caja caja);
        Task<bool> Actualizar(Caja caja);
    }
}
