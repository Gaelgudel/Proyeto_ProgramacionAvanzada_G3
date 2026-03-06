using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Models;

namespace ProyectoG3.Web.Pages.Bitacora
{
    public class IndexModel : PageModel
    {
        private readonly IBitacoraService _bitacoraService;

        public IndexModel(IBitacoraService bitacoraService)

        {
            _bitacoraService = bitacoraService;
        }

        public IEnumerable<BitacoraEvento> Eventos { get; set; } = new List<BitacoraEvento>();

        public async Task OnGetAsync()
        {
            Eventos = await _bitacoraService.ListarBitacoraAsync();
        }
    }
}
