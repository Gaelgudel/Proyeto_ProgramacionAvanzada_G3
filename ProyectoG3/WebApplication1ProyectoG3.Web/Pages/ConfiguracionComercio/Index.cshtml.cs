using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;

namespace ProyectoG3.Web.Pages.ConfiguracionComercio
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracionComercioService _service;
        private readonly IComercioService _comercioService;

        public IndexModel(IConfiguracionComercioService service, IComercioService comercioService)
        {
            _service = service;
            _comercioService = comercioService;
        }

        public List<ConfiguracionComercioListDto> Configuraciones { get; set; } = new();
        public ComercioDetailsDto? Comercio { get; set; }
        public int IdComercio { get; set; }

        public async Task<IActionResult> OnGetAsync(int idComercio)
        {
            IdComercio = idComercio;
            Comercio = await _comercioService.GetByIdAsync(idComercio);

            if (Comercio is null)
                return RedirectToPage("/Comercios/Index");

            Configuraciones = await _service.GetAllByComercioAsync(idComercio);
            return Page();
        }
    }
}