using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Infrastructure.Persistence;
using ProyectoG3.Models;

namespace ProyectoG3.Web.Pages.Cajas;

public class RegistrarModel : PageModel
{
    private readonly AppDbContext _context;

    public RegistrarModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Caja Caja { get; set; }

    public int IdComercio { get; set; }

    public void OnGet(int idComercio)
    {
        IdComercio = idComercio;

        Caja = new Caja
        {
            IdComercio = idComercio
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Caja.FechaDeRegistro = DateTime.Now;

        _context.Cajas.Add(Caja);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Cajas/Index", new { idComercio = Caja.IdComercio });
    }
}