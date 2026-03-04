csharp ProyectoG3.Domain.Entities\Comercio.cs
using System;
using System.Collections.Generic;
using ProyectoG3.Models;

namespace ProyectoG3.Domain.Entities
{
    public class Comercio
    {
        public int IdComercio { get; set; }
        public string Identificacion { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public TipoComercio TipoDeComercio { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
        public virtual ICollection<Caja> Cajas { get; set; } = new HashSet<Caja>();
    }
}