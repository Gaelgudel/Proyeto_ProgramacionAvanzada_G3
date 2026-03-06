using System.ComponentModel.DataAnnotations;

namespace ProyectoG3.Models
{
    public class BitacoraEvento
    {
        [Key]
        public int IdEvento { get; set; }
        public string TablaDeEvento { get; set; } = null!;
        public string TipoDeEvento { get; set; } = null!;
        public DateTime FechaDeEvento { get; set; }
        public string DescripcionDeEvento { get; set; } = null!;
        public string StackTrace { get; set; } = null!;
        public required string? DatosAnteriores { get; set; }
        public string? DatosPosteriores { get; set; }
    }

}