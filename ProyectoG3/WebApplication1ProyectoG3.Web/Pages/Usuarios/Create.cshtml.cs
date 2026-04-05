using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;

namespace ProyectoG3.Web.Pages.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly IUsuarioService _service;

        public CreateModel(IUsuarioService service)
        {
            _service = service;
        }

        [BindProperty]
        public UsuarioCreateDto Usuario { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var resultado = await _service.RegistrarAsync(Usuario);

            if (!resultado)
            {
                ModelState.AddModelError("", "Ya existe un usuario con esa identificación");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}