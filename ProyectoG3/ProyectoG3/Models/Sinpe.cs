namespace ProyectoG3.Models
{
    public class Sinpe
    {
        public int IdSinpe { get; set; }

        public string TelefonoOrigen { get; set; } = string.Empty;
        public string NombreOrigen { get; set; } = string.Empty;

        public string TelefonoDestino { get; set; } = string.Empty;
        public string NombreDestino { get; set; } = string.Empty;

        public decimal Monto { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public int Estado { get; set; } // 0 = No sincronizado | 1 = Sincronizado
    }
}