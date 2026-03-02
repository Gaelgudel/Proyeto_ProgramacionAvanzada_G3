namespace ProyectoG3.Services
{
    public interface IBitacoraService
    {
        Task RegistrarEvento(string tabla, string tipo, string descripcion, object anterior = null, object posterior = null, Exception ex = null);
    }
}