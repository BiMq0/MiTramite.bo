using MiTramite_Shared.DTOs.Reportes;

namespace MiTramite_Front.WAMiTramiteGestion.Services.Pdf
{
    public interface IPdfService
    {
        byte[] GenerateReport(ReporteDashboardDTO data, int year);
    }
}
