using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProyectoG3.Domain.Enums;

namespace ProyectoG3.Application.DTOs
{
    public class ComercioListDto
    {
        public int IdComercio { get; set; }
        public string Identificacion { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public TipoComercio TipoDeComercio { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public bool Estado { get; set; }
    }
}