using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Models;

namespace ProyectoG3.Web.Pages.Cajas
{
    public class EditarModel : PageModel
    {
        private readonly ICajaService _cajaService;

        public EditarModel(ICajaService cajaService)
        {
            _cajaService = cajaService;
        }

        [BindProperty]
        public Caja Caja { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var caja = await _cajaService.ObtenerPorId(id);

            if (caja is null)
                return NotFound();

            Caja = caja;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            //teléfono activo único excluye esta caja
            bool telefonoOcupado = await _cajaService.ExistsByTelefonoActivoAsync(
                Caja.TelefonoSINPE, Caja.IdCaja);

            if (telefonoOcupado)
            {
                ModelState.AddModelError(nameof(Caja.TelefonoSINPE),
                    "Ya existe otra caja activa con ese número de teléfono SINPE.");
                return Page();
            }

            // Validar nombre único por comercio
            bool nombreOcupado = await _cajaService.ExistsByNombreAndComercioAsync(
                Caja.Nombre, Caja.IdComercio, Caja.IdCaja);

            if (nombreOcupado)
            {
                ModelState.AddModelError(nameof(Caja.Nombre),
                    "Ya existe otra caja con ese nombre para este comercio.");
                return Page();
            }

            await _cajaService.UpdateAsync(Caja);

            TempData["Success"] = "Caja actualizada correctamente.";
            return RedirectToPage("Index", new { idComercio = Caja.IdComercio });
        }
    }
}