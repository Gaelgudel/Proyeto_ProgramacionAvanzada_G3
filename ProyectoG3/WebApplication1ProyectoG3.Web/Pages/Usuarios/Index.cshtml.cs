using ProyectoG3.Application.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Domain.Entities;


namespace ProyectoG3.Web.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly IUsuarioService _service;

        public IndexModel(IUsuarioService service)
        {
            _service = service;
        }

        public List<UsuarioDto> Usuarios { get; set; }
        public async Task OnGetAsync()
        {
            Usuarios = await _service.ListarAsync();
        }
    }
}