using System.ComponentModel.DataAnnotations;

namespace ProyectoG3.Application.DTOs
{
    public class UsuarioUpdateDto
    {
        public int IdUsuario { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string PrimerApellido { get; set; }

        [Required]
        public string SegundoApellido { get; set; }

        [Required]
        public string Identificacion { get; set; }

        [Required]
        public string CorreoElectronico { get; set; }

        public bool Estado { get; set; }
    }
}