using ProyectoG3.Models;

namespace ProyectoG3.Services
{

    namespace ProyectoSinpe.Services
    {
        public interface ICajaService
        {
            Task<IEnumerable<Caja>> ListarPorComercio(int idComercio);

            Task<Caja> ObtenerPorId(int idCaja);
            Task EditarCaja(Caja modelo);
            Task RegistrarCaja(Caja modelo);
        }
    }
}
