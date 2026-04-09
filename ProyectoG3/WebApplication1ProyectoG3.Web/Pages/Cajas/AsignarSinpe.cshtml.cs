using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoG3.Infrastructure.Persistence;
using ProyectoG3.Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace ProyectoG3.Web.Pages.Cajas
{
    public class AsignarSinpeModel : PageModel
    {
        private readonly AppDbContext _context;

        public AsignarSinpeModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int IdSinpe { get; set; }

        [BindProperty]
        public int IdCaja { get; set; }

        public List<SelectListItem> Cajas { get; set; }

        public void OnGet(int idSinpe)
        {
            IdSinpe = idSinpe;

            Cajas = _context.Cajas
                .Select(c => new SelectListItem
                {
                    Value = c.IdCaja.ToString(),
                    Text = c.Nombre
                }).ToList();
        }

        public IActionResult OnPost()
        {
            var sinpe = _context.Sinpes.FirstOrDefault(s => s.IdSinpe == IdSinpe);

            if (sinpe == null)
            {
                return NotFound();
            }

            //Ligar Sinpe
            sinpe.IdCaja = IdCaja;

            _context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}