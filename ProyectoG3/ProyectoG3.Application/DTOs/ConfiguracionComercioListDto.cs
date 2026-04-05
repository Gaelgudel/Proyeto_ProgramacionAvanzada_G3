namespace ProyectoG3.Application.DTOs
{
    public class ConfiguracionComercioListDto
    {
        public int IdConfiguracion { get; set; }
        public int IdComercio { get; set; }
        public string NombreComercio { get; set; } = string.Empty;
        public int TipoConfiguracion { get; set; }
        public string TipoConfiguracionTexto { get; set; } = string.Empty;
        public int Comision { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
    }
}