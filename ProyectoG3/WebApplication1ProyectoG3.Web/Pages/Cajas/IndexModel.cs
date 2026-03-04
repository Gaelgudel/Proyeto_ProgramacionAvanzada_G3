using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoG3.Domain.Entities;
using ProyectoG3.Infrastructure.Persistence;
using ProyectoG3.Models;

namespace ProyectoG3.Web.Pages.Cajas;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public string NombreComercio { get; set; } = string.Empty;
    public int IdComercio { get; set; }
    public List<Caja> Cajas { get; set; } = new List<Caja>();

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int idComercio)
    {
        try
        {
            IdComercio = idComercio;

            // Obtener comercio para mostrar el nombre
            var comercio = await _context.Comercios
                .FirstOrDefaultAsync(c => c.IdComercio == idComercio);

            if (comercio == null)
            {
                TempData["Error"] = "Comercio no encontrado";
                return RedirectToPage("../Comercios/Index");
            }

            NombreComercio = comercio.Nombre;

            Cajas = await _context.Cajas
                .Where(c => c.IdComercio == idComercio)
                .OrderByDescending(c => c.FechaDeRegistro)
                .ToListAsync();

            return Page();
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Error: " + ex.Message;
            return RedirectToPage("../Comercios/Index");
        }
    }
}