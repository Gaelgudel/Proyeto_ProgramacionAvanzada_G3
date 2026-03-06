using ProyectoG3.Models;


namespace ProyectoG3.Application.Interfaces
{
    public interface ICajaService
    {
        Task<IEnumerable<Caja>> ListarPorComercio(int idComercio);
        Task<Caja?> ObtenerPorId(int id);
        Task<Caja> CreateAsync(Caja caja);
        Task UpdateAsync(Caja caja);
        Task<bool> ExistsByTelefonoActivoAsync(string telefonoSINPE, int? excludeId = null);
        Task<bool> ExistsByNombreAndComercioAsync(string nombre, int idComercio, int? excludeId = null);
    }
}

