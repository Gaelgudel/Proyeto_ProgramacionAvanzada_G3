using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Enums;

namespace WebApplication1ProyectoG3.Web.Pages.Comercios
{
    public class EditModel : PageModel
    {
        private readonly IComercioService _service;

        public EditModel(IComercioService service)
        {
            _service = service;
        }

        [BindProperty]
        public ComercioUpdateDto Comercio { get; set; } = new();

        public List<TipoComercio> TiposComercio { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TiposComercio = Enum.GetValues<TipoComercio>().ToList();

            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                TempData["Error"] = "No se encontró el comercio.";
                return RedirectToPage("Index");
            }

            Comercio.IdComercio = data.IdComercio;
            Comercio.Nombre = data.Nombre;
            Comercio.TipoDeComercio = data.TipoDeComercio;
            Comercio.Telefono = data.Telefono;
            Comercio.CorreoElectronico = data.CorreoElectronico;
            Comercio.Direccion = data.Direccion;
            Comercio.Estado = data.Estado;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TiposComercio = Enum.GetValues<TipoComercio>().ToList();

            if (!ModelState.IsValid)
                return Page();

            var result = await _service.UpdateAsync(Comercio);

            if (!result.Ok)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }

            TempData["Success"] = "Comercio actualizado correctamente.";
            return RedirectToPage("Index");
        }
    }
}