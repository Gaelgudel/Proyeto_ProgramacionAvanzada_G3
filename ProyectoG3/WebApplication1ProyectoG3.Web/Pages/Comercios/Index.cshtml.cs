using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;

namespace WebApplication1ProyectoG3.Web.Pages.Comercios
{
    public class IndexModel : PageModel
    {
        private readonly IComercioService _comercioService;

        public IndexModel(IComercioService comercioService)
        {
            _comercioService = comercioService;
        }

        public List<ComercioListDto> Comercios { get; set; } = new();

        public async Task OnGetAsync()
        {
            Comercios = await _comercioService.GetAllAsync();
        }
    }
}