using System.Threading.Tasks;
using ProyectoG3.Models;

namespace ProyectoG3.Repository
{
    public interface IBitacoraRepository
    {
        // Inserta un nuevo evento en la tabla BITACORA_EVENTOS
        Task Insertar(BitacoraEvento evento);
    }
}