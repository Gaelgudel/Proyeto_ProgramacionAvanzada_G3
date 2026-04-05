using ProyectoG3.Domain.Enums;
using ProyectoG3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoG3.Domain.Entities
{
    public class Comercio
    {
        [Key]
        public int IdComercio { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public int TipoIdentificacion { get; set; } 
        public string Nombre { get; set; } = string.Empty;
        public int TipoDeComercio { get; set; } // 1 - Restaurantes, 2 - Supermercados, 3 - Ferreterías, 4 - Otros
        public string Telefono { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; } 
        public virtual ICollection<Caja> CAJAS { get; set; } = new List<Caja>();
        public virtual ConfiguracionComercio? Configuracion { get; set; }


    }
}
