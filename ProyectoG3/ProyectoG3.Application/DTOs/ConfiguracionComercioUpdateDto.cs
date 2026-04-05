using System.ComponentModel.DataAnnotations;

namespace ProyectoG3.Application.DTOs
{
    public class ConfiguracionComercioUpdateDto
    {
        [Required]
        public int IdConfiguracion { get; set; }

        [Required(ErrorMessage = "El tipo de configuración es requerido.")]
        [Range(1, 3, ErrorMessage = "El tipo de configuración debe ser 1, 2 o 3.")]
        public int TipoConfiguracion { get; set; }

        [Required(ErrorMessage = "La comisión es requerida.")]
        [Range(1, 100, ErrorMessage = "La comisión debe estar entre 1 y 100.")]
        public int Comision { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}