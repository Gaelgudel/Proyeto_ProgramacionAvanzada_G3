using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;

namespace ProyectoG3.Web.Pages.SINPE
{
    public class RegistrarModel : PageModel
    {
        private readonly ISinpeService _sinpeService;

        public RegistrarModel(ISinpeService sinpeService)
        {
            _sinpeService = sinpeService;
        }

        [BindProperty]
        public SinpeCreateDto Sinpe { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var resultado = await _sinpeService.RegistrarAsync(Sinpe);

            if (!resultado)
            {
                ModelState.AddModelError("", "La caja no existe o está inactiva.");
                return Page();
            }

            TempData["Mensaje"] = "Pago registrado correctamente.";
            return RedirectToPage("/SINPE/Registrar");
        }
    }
}