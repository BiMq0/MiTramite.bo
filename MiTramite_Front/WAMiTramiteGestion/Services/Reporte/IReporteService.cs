using MiTramite_Shared.DTOs.Reportes;
using System.Threading.Tasks;

namespace MiTramite_Front.WAMiTramiteGestion.Services.Reporte
{
    public interface IReporteService
    {
        Task<ReporteDashboardDTO> GetDashboardDataAsync(int year);
    }
}
