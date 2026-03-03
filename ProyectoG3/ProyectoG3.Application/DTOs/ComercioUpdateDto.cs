using System.ComponentModel.DataAnnotations;
using ProyectoG3.Domain.Enums;

namespace ProyectoG3.Application.DTOs
{
    public class ComercioUpdateDto
    {
        [Required]
        public int IdComercio { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El tipo de comercio es obligatorio.")]
        public TipoComercio TipoDeComercio { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [StringLength(200, ErrorMessage = "El correo no puede exceder 200 caracteres.")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(500, ErrorMessage = "La dirección no puede exceder 500 caracteres.")]
        public string Direccion { get; set; }

        public bool Estado { get; set; }
    }
}