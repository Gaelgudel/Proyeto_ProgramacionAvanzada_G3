using System.ComponentModel.DataAnnotations;
namespace ProyectoG3.Application.DTOs
{
    public class SinpeCreateDto
    {
        [Required]
        [StringLength(10)]
        public string TelefonoOrigen { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreOrigen { get; set; }

        [Required]
        [StringLength(10)]
        public string TelefonoDestinatario { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreDestinatario { get; set; }

        [Required]
        [Range(0.01, 999999999)]
        public decimal Monto { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }
    }
}
