using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;

namespace ProyectoG3.Web.Pages.ConfiguracionComercio
{
    public class EditModel : PageModel
    {
        private readonly IConfiguracionComercioService _service;
        private readonly IComercioService _comercioService;

        public EditModel(IConfiguracionComercioService service, IComercioService comercioService)
        {
            _service = service;
            _comercioService = comercioService;
        }

        [BindProperty]
        public ConfiguracionComercioUpdateDto Configuracion { get; set; } = new();

        public ComercioDetailsDto? Comercio { get; set; }
        public int IdComercio { get; set; }

        public List<SelectListItem> TiposConfiguracion { get; set; } = new()
        {
            new SelectListItem { Value = "1", Text = "Plataforma" },
            new SelectListItem { Value = "2", Text = "Externa" },
            new SelectListItem { Value = "3", Text = "Ambas" }
        };

        public async Task<IActionResult> OnGetAsync(int idConfiguracion, int idComercio)
        {
            IdComercio = idComercio;
            Comercio = await _comercioService.GetByIdAsync(idComercio);

            if (Comercio is null)
                return RedirectToPage("/Comercios/Index");

            var config = await _service.GetByIdAsync(idConfiguracion);

            if (config is null)
                return RedirectToPage("Index", new { idComercio });

            Configuracion = new ConfiguracionComercioUpdateDto
            {
                IdConfiguracion = config.IdConfiguracion,
                TipoConfiguracion = config.TipoConfiguracion,
                Comision = config.Comision,
                Estado = config.Estado
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int idComercio)
        {
            IdComercio = idComercio;
            Comercio = await _comercioService.GetByIdAsync(idComercio);

            if (!ModelState.IsValid)
                return Page();

            var result = await _service.UpdateAsync(Configuracion);

            if (!result.Ok)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }

            TempData["Success"] = "Configuración actualizada correctamente.";
            return RedirectToPage("Index", new { idComercio });
        }
    }
}