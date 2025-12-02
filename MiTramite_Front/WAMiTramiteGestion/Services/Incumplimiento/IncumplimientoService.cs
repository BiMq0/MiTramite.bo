using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.Incumplimiento;
using MiTramite_Shared.Endpoints;

namespace WAMiTramiteGestion.Services.Incumplimiento
{
    public class IncumplimientoService : IIncumplimientoService
    {
        private readonly HttpClient _httpClient;

        public IncumplimientoService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("ApiClient");
        }

        public async Task<List<IncumplimientoRegistroDTO>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<IncumplimientoRegistroDTO>>(IncumplimientoEndpoints.ObtenerTodos);
            return response ?? new List<IncumplimientoRegistroDTO>();
        }

        public async Task<IncumplimientoDetalleDTO?> ObtenerPorIdTramiteAsync(long idSolicitudTramite)
        {
            try
            {
                var url = IncumplimientoEndpoints.ObtenerPorId.Replace("{idSolicitudTramite}", idSolicitudTramite.ToString());
                return await _httpClient.GetFromJsonAsync<IncumplimientoDetalleDTO>(url);
            }
            catch
            {
                return null;
            }
        }
    }
}
