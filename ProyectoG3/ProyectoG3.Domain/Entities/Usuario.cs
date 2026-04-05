using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoG3.Domain.Entities
{
    [Table("UsuariosG3")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public int IdComercio { get; set; }

        public Guid? IdNetUser { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string PrimerApellido { get; set; }

        [Required]
        [StringLength(100)]
        public string SegundoApellido { get; set; }

        [Required]
        [StringLength(10)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(200)]
        public string CorreoElectronico { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        public bool Estado { get; set; } = true;
    }
}