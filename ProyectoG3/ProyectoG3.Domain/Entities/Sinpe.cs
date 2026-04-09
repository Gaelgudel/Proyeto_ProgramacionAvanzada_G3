using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoG3.Domain.Entities
{
    public class Sinpe
    {
        [Key]
        public int IdSinpe { get; set; }

        [Required(ErrorMessage = "Debe ingresar el teléfono de origen")]
        [StringLength(10, ErrorMessage = "El teléfono no puede tener más de 10 caracteres")]
        public required string TelefonoOrigen { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de origen")]
        [StringLength(200, ErrorMessage = "El nombre no puede superar 200 caracteres")]
        public required string NombreOrigen { get; set; }

        [Required(ErrorMessage = "Debe ingresar el teléfono del destinatario")]
        [StringLength(10, ErrorMessage = "El teléfono no puede tener más de 10 caracteres")]
        public required string TelefonoDestinatario { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del destinatario")]
        [StringLength(200, ErrorMessage = "El nombre no puede superar 200 caracteres")]
        public required string NombreDestinatario { get; set; }

        [Required(ErrorMessage = "Debe ingresar un monto")]
        [Range(0.01, 999999999, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [StringLength(50, ErrorMessage = "La descripción no puede superar 50 caracteres")]
        public string? Descripcion { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public bool Estado { get; set; } = false;

        public int? IdCaja { get; set; }
    }
}