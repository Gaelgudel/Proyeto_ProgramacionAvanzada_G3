using ProyectoG3.Application.DTOs;

namespace ProyectoG3.Application.Interfaces
{
    public interface IReporteMensualService
    {
        Task<List<ReporteMensualListDto>> GetAllAsync();

        Task GenerarReportesMensualesAsync();
    }
}