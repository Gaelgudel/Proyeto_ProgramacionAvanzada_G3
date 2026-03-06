using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Models;
using ProyectoG3.Infrastructure.Persistence;

namespace ProyectoG3.Web.Pages.Cajas
{
    public class VerSinpeModel : PageModel
    {
        private readonly ICajaService _cajaService;
        private readonly AppDbContext _context;

        public VerSinpeModel(ICajaService cajaService, AppDbContext context)
        {
            _cajaService = cajaService;
            _context = context;
        }

        public Caja Caja { get; set; } = null!;
        public IEnumerable<Sinpe> Sinpes { get; set; } = Enumerable.Empty<Sinpe>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Buscar la caja por IdCaja 
            var caja = await _cajaService.ObtenerPorId(id);


            if (caja is null)
                return NotFound();

            Caja = caja;

            // Filtra SINPE por el teléfono destinatario de la caja,
            // ordenados del más reciente al más antiguo según requerimiento
            Sinpes = await _context.Sinpes
                .Where(s => s.TelefonoDestinatario == caja.TelefonoSINPE)
                .OrderByDescending(s => s.FechaDeRegistro)
                .ToListAsync();

            return Page();
        }
    }
}