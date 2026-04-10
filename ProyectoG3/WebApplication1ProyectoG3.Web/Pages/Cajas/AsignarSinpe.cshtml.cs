using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoG3.Infrastructure.Persistence;
using ProyectoG3.Domain.Entities;

namespace ProyectoG3.Web.Pages.Cajas
{
    public class AsignarSinpeModel : PageModel
    {
        private readonly AppDbContext _context;

        public AsignarSinpeModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int IdSinpe { get; set; }

        [BindProperty]
        public int IdCaja { get; set; }

        [BindProperty]
        public int IdCajaOrigen { get; set; }

        // IdComercio para saber a dónde volver si no hay caja asignada
        [BindProperty]
        public int IdComercioOrigen { get; set; }

        public List<SelectListItem> Cajas { get; set; } = new();

        public void OnGet(int idSinpe)
        {
            IdSinpe = idSinpe;

            var sinpe = _context.Sinpes.FirstOrDefault(s => s.IdSinpe == idSinpe);

            if (sinpe != null && sinpe.IdCaja.HasValue)
            {
                // Si ya tiene caja asignada, guardamos su id y el comercio al que pertenece
                IdCajaOrigen = sinpe.IdCaja.Value;
                var caja = _context.Cajas.FirstOrDefault(c => c.IdCaja == sinpe.IdCaja.Value);
                if (caja != null)
                    IdComercioOrigen = caja.IdComercio;
            }
            else if (sinpe != null)
            {
                // Si no tiene caja, buscamos por teléfono destinatario
                var caja = _context.Cajas
                    .FirstOrDefault(c => c.TelefonoSINPE == sinpe.TelefonoDestinatario);
                if (caja != null)
                {
                    IdCajaOrigen = caja.IdCaja;
                    IdComercioOrigen = caja.IdComercio;
                }
            }

            Cajas = _context.Cajas
                .Select(c => new SelectListItem
                {
                    Value = c.IdCaja.ToString(),
                    Text = c.Nombre
                }).ToList();
        }

        public IActionResult OnPost()
        {
            var sinpe = _context.Sinpes.FirstOrDefault(s => s.IdSinpe == IdSinpe);

            if (sinpe == null)
                return NotFound();

            sinpe.IdCaja = IdCaja;
            _context.SaveChanges();

            // Si tenemos el id de la caja origen volvemos a VerSinpe
            // Si no, volvemos a Comercios
            if (IdCajaOrigen > 0)
                return RedirectToPage("/Cajas/VerSinpe", new { id = IdCajaOrigen });

            return RedirectToPage("/Comercios/Index");
        }
    }
}