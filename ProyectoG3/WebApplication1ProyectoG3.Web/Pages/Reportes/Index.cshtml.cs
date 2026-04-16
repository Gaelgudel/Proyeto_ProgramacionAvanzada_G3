using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoG3.Application.DTOs;
using ProyectoG3.Application.Interfaces;

namespace ProyectoG3.Web.Pages.Reportes
{
    public class IndexModel : PageModel
    {
        private readonly IReporteMensualService _reporteMensualService;

        public IndexModel(IReporteMensualService reporteMensualService)
        {
            _reporteMensualService = reporteMensualService;
        }

        public List<ReporteMensualListDto> Reportes { get; set; } = new();

        public async Task OnGetAsync()
        {
            Reportes = await _reporteMensualService.GetAllAsync();
        }

        public async Task<IActionResult> OnPostGenerarAsync()
        {
            await _reporteMensualService.GenerarReportesMensualesAsync();
            TempData["Mensaje"] = "Los reportes mensuales fueron generados o actualizados correctamente.";
            return RedirectToPage();
        }
    }
}