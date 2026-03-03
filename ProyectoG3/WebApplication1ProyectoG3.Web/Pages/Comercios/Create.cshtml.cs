using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Enums;

namespace WebApplication1ProyectoG3.Web.Pages.Comercios
{
    public class CreateModel : PageModel
    {
        private readonly IComercioService _service;

        public CreateModel(IComercioService service)
        {
            _service = service;
        }

        [BindProperty]
        public ComercioCreateDto Comercio { get; set; } = new();

        public List<TipoIdentificacion> TiposIdentificacion { get; set; } = new();
        public List<TipoComercio> TiposComercio { get; set; } = new();

        public void OnGet()
        {
            TiposIdentificacion = Enum.GetValues<TipoIdentificacion>().ToList();
            TiposComercio = Enum.GetValues<TipoComercio>().ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TiposIdentificacion = Enum.GetValues<TipoIdentificacion>().ToList();
            TiposComercio = Enum.GetValues<TipoComercio>().ToList();

            if (!ModelState.IsValid)
                return Page();

            var result = await _service.CreateAsync(Comercio);

            if (!result.Ok)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }

            TempData["Success"] = "Comercio registrado correctamente.";
            return RedirectToPage("Index");
        }
    }
}