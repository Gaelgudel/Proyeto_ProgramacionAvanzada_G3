using System;
using ProyectoG3.Models;

namespace ProyectoG3.Repository
{
    public class CajaRepository : ICajaRepository
    {
        private readonly string _connectionString;

        public CajaRepository(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no se encontró.");
        }

        public async Task<IEnumerable<Caja>> ObtenerCajasPorComercio(int idComercio)
        {
            // Implementacion de busqueda por ID de comercio
            return new List<Caja>(); 
        }

        public async Task<bool> ValidarNombreEnComercio(string nombre, int idComercio)
        {
            
            return false;
        }

        public async Task<bool> ValidarTelefonoGlobal(string telefono)
        {
            
            return false;
        }

        public async Task<Caja> ObtenerPorId(int id) { return null; }
        public async Task<bool> Insertar(Caja caja) { return true; }
        public async Task<bool> Actualizar(Caja caja) { return true; }
    }
}
