using System;
using System.ComponentModel.DataAnnotations;


namespace ProyectoG3.Domain.Entities
{
    public class Sinpe
    {
        [Key]
        public int IdSinpe { get; set; }

        [Required]
        [StringLength(10)]
        public required string TelefonoOrigen { get; set; }

        [Required]
        [StringLength(200)]
        public required string NombreOrigen { get; set; }

        [Required]
        [StringLength(10)]
        public required string TelefonoDestinatario { get; set; }

        [Required]
        [StringLength(200)]
        public required string NombreDestinatario { get; set; }

        [Required]
        [Range(0.01, 999999999)]
        public decimal Monto { get; set; }

        [StringLength(50)]
        public string? Descripcion { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public bool Estado { get; set; } = false; // false = No sincronizado
    }
}

