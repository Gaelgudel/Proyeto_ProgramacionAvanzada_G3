using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;

namespace WebApplication1ProyectoG3.Web.Pages.Comercios
{
    public class DetailsModel : PageModel
    {
        private readonly IComercioService _service;

        public DetailsModel(IComercioService service)
        {
            _service = service;
        }

        public ComercioDetailsDto? Comercio { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Comercio = await _service.GetByIdAsync(id);

            if (Comercio == null)
            {
                TempData["Error"] = "No se encontró el comercio.";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}