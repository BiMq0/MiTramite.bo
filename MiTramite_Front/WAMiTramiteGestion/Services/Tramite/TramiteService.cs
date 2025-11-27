using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;
using MiTramite_Shared.Endpoints;

namespace WAMiTramiteGestion.Services
{
    public class TramiteService : ITramiteService
    {
        private readonly HttpClient _httpClient;

        public TramiteService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        #region Métodos de Funcionario

        public async Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorFuncionarioAsync(long idFuncionario)
        {
            try
            {
                var url = $"{SolicitudTramiteEndpoints.BASE}{SolicitudTramiteEndpoints.OBTENER_TRAMITES_POR_FUNCIONARIO}";
                url = url.Replace("{idFuncionario}", idFuncionario.ToString());
                var response = await _httpClient.GetFromJsonAsync<List<SolicitudTramiteRegistroDTO>>(url);
                return response ?? new List<SolicitudTramiteRegistroDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener trámites del funcionario: {ex.Message}");
                return new List<SolicitudTramiteRegistroDTO>();
            }
        }

        public async Task<SolicitudTramiteRegistroDTO?> ObtenerTramitePorIdAsync(long idSolicitudTramite)
        {
            try
            {
                var url = $"{SolicitudTramiteEndpoints.BASE}{SolicitudTramiteEndpoints.OBTENER_TRAMITE_POR_ID}";
                url = url.Replace("{idTramite}", idSolicitudTramite.ToString());
                var response = await _httpClient.GetFromJsonAsync<SolicitudTramiteRegistroDTO>(url);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener detalles del trámite: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CompletarTramiteAsync(long idSolicitudTramite)
        {
            try
            {
                var url = $"{SolicitudTramiteEndpoints.BASE}{SolicitudTramiteEndpoints.COMPLETAR_TRAMITES}";
                url = url.Replace("{idTramite}", idSolicitudTramite.ToString());
                var response = await _httpClient.PostAsJsonAsync(url, new { });
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al completar trámite: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RechazarTramiteAsync(long idSolicitudTramite, string motivo)
        {
            try
            {
                var url = $"{SolicitudTramiteEndpoints.BASE}{SolicitudTramiteEndpoints.RECHAZAR_TRAMITE}";
                url = url.Replace("{idTramite}", idSolicitudTramite.ToString());
                var response = await _httpClient.PostAsJsonAsync(url, motivo);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al rechazar trámite: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Métodos de Gerente

        public async Task<List<SolicitudTramiteRegistroDTO>> ObtenerTodosLosTramitesAsync()
        {
            try
            {
                var url = $"{SolicitudTramiteEndpoints.BASE}{SolicitudTramiteEndpoints.OBTENER_TODOS}";
                var response = await _httpClient.GetFromJsonAsync<List<SolicitudTramiteRegistroDTO>>(url);
                return response ?? new List<SolicitudTramiteRegistroDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los trámites: {ex.Message}");
                return new List<SolicitudTramiteRegistroDTO>();
            }
        }

        #endregion
    }
}
