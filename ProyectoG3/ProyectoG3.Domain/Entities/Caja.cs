using ProyectoG3.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProyectoG3.Models
{
    public class Caja
    {
        [Key]
        public int IdCaja { get; set; }
        public int IdComercio { get; set; }
        public required string Nombre { get; set; }
        public required string TelefonoSINPE { get; set; }
        public required string Descripcion { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
        public virtual Comercio? Comercio { get; set; }
        public virtual ICollection<Sinpe> Sinpes { get; set; } = new List<Sinpe>();
    }
}

