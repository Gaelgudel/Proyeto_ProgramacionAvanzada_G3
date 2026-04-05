using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;

namespace ProyectoG3.Web.Pages.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly IUsuarioService _service;

        public EditModel(IUsuarioService service)
        {
            _service = service;
        }

        [BindProperty]
        public UsuarioUpdateDto Usuario { get; set; }   

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var data = await _service.ObtenerPorIdAsync(id);

            if (data == null)
                return RedirectToPage("Index");

            Usuario = new UsuarioUpdateDto
            {
                IdUsuario = data.IdUsuario,  
                Nombres = data.Nombres,
                PrimerApellido = data.PrimerApellido,
                SegundoApellido = data.SegundoApellido,
                Identificacion = data.Identificacion,
                CorreoElectronico = data.CorreoElectronico,
                Estado = data.Estado
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var resultado = await _service.EditarAsync(id, Usuario);

            if (!resultado)
            {
                ModelState.AddModelError("", "Identificación duplicada");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}