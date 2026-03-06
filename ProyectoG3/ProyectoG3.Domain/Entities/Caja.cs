using ProyectoG3.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoG3.Models
{
    [Table("CAJAS")]
    public class Caja
    {
        [Key]
        public int IdCaja { get; set; }

        [ForeignKey("Comercio")]
        public int IdComercio { get; set; }

        public string Nombre { get; set; }

        public string TelefonoSINPE { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        public bool Estado { get; set; }

        public virtual Comercio? Comercio { get; set; }
    }
}