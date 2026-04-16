using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Infrastructure.Persistence;
using System.Linq;

namespace ProyectoG3.Infrastructure.Services
{
    public class ReporteMensualService : IReporteMensualService
    {
        private readonly AppDbContext _context;

        public ReporteMensualService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReporteMensualListDto>> GetAllAsync()
        {
            return await _context.ReportesMensuales
                .Join(_context.Comercios,
                      r => r.IdComercio,
                      c => c.IdComercio,
                      (r, c) => new ReporteMensualListDto
                      {
                          IdReporte = r.IdReporte,
                          IdComercio = r.IdComercio,
                          NombreComercio = c.Nombre,
                          CantidadDeCajas = r.CantidadDeCajas,
                          MontoTotalRecaudado = r.MontoTotalRecaudado,
                          CantidadDeSINPES = r.CantidadDeSINPES,
                          MontoTotalComision = r.MontoTotalComision,
                          FechaDelReporte = r.FechaDelReporte
                      })
                .OrderByDescending(x => x.FechaDelReporte)
                .ToListAsync();
        }

        public async Task GenerarReportesMensualesAsync()
        {
            var hoy = DateTime.Now;
            var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
            var finMes = inicioMes.AddMonths(1);

            var comercios = await _context.Comercios.ToListAsync();

            foreach (var comercio in comercios)
            {
                var cajasDelComercio = await _context.Cajas
                    .Where(c => c.IdComercio == comercio.IdComercio)
                    .ToListAsync();

                var idsCajas = cajasDelComercio.Select(c => c.IdCaja).ToList();

                var sinpesDelMes = await _context.Sinpes
    .Where(s => s.IdCaja.HasValue
             && idsCajas.Contains(s.IdCaja.Value)
             && s.FechaDeRegistro >= inicioMes
             && s.FechaDeRegistro < finMes)
    .ToListAsync();

                int cantidadDeCajas = cajasDelComercio.Count;
                int cantidadDeSINPES = sinpesDelMes.Count;
                decimal montoTotalRecaudado = sinpesDelMes.Sum(s => s.Monto);

                var configuracion = await _context.ConfiguracionesComercio
                    .FirstOrDefaultAsync(c => c.IdComercio == comercio.IdComercio && c.Estado);

                decimal porcentajeComision = 0m;

                if (configuracion != null)
                {
                    porcentajeComision = configuracion.Comision / 100m;
                }

                decimal montoTotalComision = montoTotalRecaudado * porcentajeComision;

                var reporteExistente = await _context.ReportesMensuales
                    .FirstOrDefaultAsync(r =>
                        r.IdComercio == comercio.IdComercio &&
                        r.FechaDelReporte.Year == hoy.Year &&
                        r.FechaDelReporte.Month == hoy.Month);

                if (reporteExistente != null)
                {
                    reporteExistente.CantidadDeCajas = cantidadDeCajas;
                    reporteExistente.CantidadDeSINPES = cantidadDeSINPES;
                    reporteExistente.MontoTotalRecaudado = montoTotalRecaudado;
                    reporteExistente.MontoTotalComision = montoTotalComision;
                    reporteExistente.FechaDelReporte = hoy;
                }
                else
                {
                    var nuevoReporte = new ReporteMensual
                    {
                        IdComercio = comercio.IdComercio,
                        CantidadDeCajas = cantidadDeCajas,
                        CantidadDeSINPES = cantidadDeSINPES,
                        MontoTotalRecaudado = montoTotalRecaudado,
                        MontoTotalComision = montoTotalComision,
                        FechaDelReporte = hoy
                    };

                    _context.ReportesMensuales.Add(nuevoReporte);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}