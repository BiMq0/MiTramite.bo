using System.Threading.Tasks;
using MiTramite_Shared.DTOs.Reportes;

namespace MiTramite_Back.Logica_De_Negocio.Services.Reportes
{
    public interface IReporteService
    {
        Task<ReporteDashboardDTO> GetDashboardDataAsync(int year);
    }
}
