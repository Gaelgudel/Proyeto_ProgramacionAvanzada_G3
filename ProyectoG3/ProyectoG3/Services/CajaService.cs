using ProyectoG3.Models;
using ProyectoG3.Repository;
using ProyectoG3.Services.ProyectoSinpe.Services;

namespace ProyectoG3.Services
{
    public class CajaService : ICajaService
    {
        private readonly ICajaRepository _repository;
        private readonly IBitacoraService _bitacora;

        public CajaService(ICajaRepository repository, IBitacoraService bitacora)
        {
            _repository = repository;
            _bitacora = bitacora;
        }

        public async Task<IEnumerable<Caja>> ListarPorComercio(int idComercio)
        {
            return await _repository.ObtenerCajasPorComercio(idComercio);
        }

        public async Task<Caja> ObtenerPorId(int id)
        {
            return await _repository.ObtenerPorId(id);
        }

        public async Task RegistrarCaja(Caja caja)
        {
            // Validaciones del enunciado
            if (await _repository.ValidarNombreEnComercio(caja.Nombre, caja.IdComercio))
                throw new Exception("Nombre de caja duplicado en este comercio.");

            if (await _repository.ValidarTelefonoGlobal(caja.TelefonoSINPE))
                throw new Exception("El teléfono SINPE ya está activo en otra caja.");

            caja.FechaDeRegistro = DateTime.Now;
            caja.Estado = true;

            await _repository.Insertar(caja);
            await _bitacora.RegistrarEvento("CAJAS", "Registrar", "Nueva caja", null, caja);
        }

        public async Task EditarCaja(Caja caja)
        {
            var anterior = await _repository.ObtenerPorId(caja.IdCaja);
            caja.FechaDeModificacion = DateTime.Now;

            await _repository.Actualizar(caja);
            await _bitacora.RegistrarEvento("CAJAS", "Editar", "Caja editada", anterior, caja);
        }
    }
}