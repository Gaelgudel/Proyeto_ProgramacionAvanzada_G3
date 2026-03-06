using System.ComponentModel.DataAnnotations;

namespace ProyectoG3.Application.DTOs
{
    public class SinpeCreateDto
    {
        [Required(ErrorMessage = "Debe ingresar el teléfono de origen")]
        [StringLength(10, ErrorMessage = "El teléfono de origen no puede tener más de 10 caracteres")]
        public string TelefonoOrigen { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de origen")]
        [StringLength(200, ErrorMessage = "El nombre de origen no puede tener más de 200 caracteres")]
        public string NombreOrigen { get; set; }

        [Required(ErrorMessage = "Debe ingresar el teléfono del destinatario")]
        [StringLength(10, ErrorMessage = "El teléfono del destinatario no puede tener más de 10 caracteres")]
        public string TelefonoDestinatario { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del destinatario")]
        [StringLength(200, ErrorMessage = "El nombre del destinatario no puede tener más de 200 caracteres")]
        public string NombreDestinatario { get; set; }

        [Required(ErrorMessage = "Debe ingresar un monto")]
        [Range(0.01, 999999999, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [StringLength(50, ErrorMessage = "La descripción no puede tener más de 50 caracteres")]
        public string? Descripcion { get; set; }
    }
}