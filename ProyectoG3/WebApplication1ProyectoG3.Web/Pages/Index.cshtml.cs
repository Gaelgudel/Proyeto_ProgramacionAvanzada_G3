using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoG3.Infrastructure.Persistence;

namespace WebApplication1ProyectoG3.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public int TotalComercios { get; set; }
        public int ComerciosActivos { get; set; }
        public int TotalCajas { get; set; }
        public int TotalSinpes { get; set; }
        public int SinpesMesActual { get; set; }
        public int ComerciosConConfig { get; set; }

        public async Task OnGetAsync()
        {
            TotalComercios = await _context.Comercios.CountAsync();
            ComerciosActivos = await _context.Comercios.CountAsync(x => x.Estado);
            TotalCajas = await _context.Cajas.CountAsync();
            TotalSinpes = await _context.Sinpes.CountAsync();
            ComerciosConConfig = await _context.ConfiguracionesComercio.CountAsync();

            var hoy = DateTime.Now;
            SinpesMesActual = await _context.Sinpes
                .CountAsync(x => x.FechaDeRegistro.Month == hoy.Month
                              && x.FechaDeRegistro.Year == hoy.Year);
        }
    }
}