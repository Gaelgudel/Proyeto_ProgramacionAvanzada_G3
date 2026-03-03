using ProyectoG3.Domain.Enums;

namespace ProyectoG3.Application.DTOs
{
    public class ComercioUpdateDto
    {
        public int IdComercio { get; set; }
        public string Nombre { get; set; }
        public TipoComercio TipoDeComercio { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
    }
}