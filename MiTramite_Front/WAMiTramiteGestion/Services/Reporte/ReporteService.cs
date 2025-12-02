using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.Reportes;
using MiTramite_Shared.Endpoints;

namespace MiTramite_Front.WAMiTramiteGestion.Services.Reporte
{
    public class ReporteService : IReporteService
    {
        private readonly HttpClient _httpClient;

        public ReporteService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("ApiClient");
        }

        public async Task<ReporteDashboardDTO> GetDashboardDataAsync(int year)
        {
            var response = await _httpClient.GetFromJsonAsync<ReporteDashboardDTO>($"{ReporteEndpoints.GetDashboardData}?year={year}");
            return response;
        }
    }
}
