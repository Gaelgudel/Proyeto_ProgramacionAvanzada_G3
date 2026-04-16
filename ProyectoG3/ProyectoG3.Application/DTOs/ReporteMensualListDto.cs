namespace ProyectoG3.Application.DTOs
{
    public class ReporteMensualListDto
    {
        public int IdReporte { get; set; }

        public int IdComercio { get; set; }

        public string NombreComercio { get; set; } = string.Empty;

        public int CantidadDeCajas { get; set; }

        public decimal MontoTotalRecaudado { get; set; }

        public int CantidadDeSINPES { get; set; }

        public decimal MontoTotalComision { get; set; }

        public DateTime FechaDelReporte { get; set; }
    }
}