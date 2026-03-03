using ProyectoG3.Models;
using ProyectoG3.Repository;
using System.Text.Json;

namespace ProyectoG3.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _repository;

        public BitacoraService(IBitacoraRepository repository)
        {
            _repository = repository;
        }

        public async Task RegistrarEvento(string tabla, string tipo, string descripcion, object anterior = null, object posterior = null, Exception ex = null)
        {
            var evento = new BitacoraEvento
            {
                TablaDeEvento = tabla,
                TipoDeEvento = tipo,
                FechaDeEvento = DateTime.Now,
                DescripcionDeEvento = ex != null ? ex.Message : descripcion,
                StackTrace = ex?.StackTrace ?? "N/A",
                // Convierte los objetos a formato JSON para almacenamiento
                DatosAnteriores = anterior != null ? JsonSerializer.Serialize(anterior) : null,
                DatosPosteriores = posterior != null ? JsonSerializer.Serialize(posterior) : null
            };

            await _repository.Insertar(evento);
        }
    }
}
