using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoG3.Domain.Entities
{
    [Table("ConfiguracionComercioG3")]
    public class ConfiguracionComercio
    {
        [Key]
        public int IdConfiguracion { get; set; }

        public int IdComercio { get; set; }

        public int TipoConfiguracion { get; set; } // 1 - Plataforma, 2 - Externa, 3 - Ambas

        public int Comision { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        public bool Estado { get; set; }

        // Navegación
        [ForeignKey("IdComercio")]
        public virtual Comercio? Comercio { get; set; }
    }
}