using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// #region DATOS DE EJEMPLO TEMPORALES - CAMBIAR A MiTramite_Shared.DTOs.TramiteDTOs CUANDO ESTÉ DISPONIBLE
using MiTramite_Front.Services.Tramite;
// #endregion
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

        public async Task<List<SolicitudTramiteDTO>> ObtenerTramitesPendientes()
        {
            try
            {
                // var url = TramiteEndpoints.BASE + TramiteEndpoints.TRAMITES_PENDIENTES;
                // var response = await _httpClient.GetFromJsonAsync<List<SolicitudTramiteDTO>>(url);
                // return response ?? new List<SolicitudTramiteDTO>();

                // TODO: Implementar cuando esté disponible el endpoint
                return new List<SolicitudTramiteDTO>();
            }
            catch (Exception ex)
            {
                // TODO: Manejar excepción
                throw;
            }
        }

        public async Task<List<SolicitudTramiteDTO>> ObtenerHistorialTramites()
        {
            try
            {
                // var url = TramiteEndpoints.BASE + TramiteEndpoints.HISTORIAL_TRAMITES;
                // var response = await _httpClient.GetFromJsonAsync<List<SolicitudTramiteDTO>>(url);
                // return response ?? new List<SolicitudTramiteDTO>();

                // TODO: Implementar cuando esté disponible el endpoint
                return new List<SolicitudTramiteDTO>();
            }
            catch (Exception ex)
            {
                // TODO: Manejar excepción
                throw;
            }
        }

        public async Task<DetallesTramiteDTO> ObtenerDetallesTramite(long idSolicitudTramite)
        {
            try
            {
                // var url = TramiteEndpoints.BASE + TramiteEndpoints.OBTENER_DETALLES;
                // url = url.Replace("{id}", idSolicitudTramite.ToString());
                // var response = await _httpClient.GetFromJsonAsync<DetallesTramiteDTO>(url);
                // return response!;

                // TODO: Implementar cuando esté disponible el endpoint
                return new DetallesTramiteDTO();
            }
            catch (Exception ex)
            {
                // TODO: Manejar excepción
                throw;
            }
        }

        public async Task<List<ArchivoTramiteDTO>> ObtenerArchivosTramite(long idSolicitudTramite)
        {
            try
            {
                // var url = TramiteEndpoints.BASE + TramiteEndpoints.OBTENER_ARCHIVOS;
                // url = url.Replace("{id}", idSolicitudTramite.ToString());
                // var response = await _httpClient.GetFromJsonAsync<List<ArchivoTramiteDTO>>(url);
                // return response ?? new List<ArchivoTramiteDTO>();

                // TODO: Implementar cuando esté disponible el endpoint
                return new List<ArchivoTramiteDTO>();
            }
            catch (Exception ex)
            {
                // TODO: Manejar excepción
                throw;
            }
        }

        public async Task<bool> AprobarTramite(long idSolicitudTramite)
        {
            try
            {
                // var url = TramiteEndpoints.BASE + TramiteEndpoints.APROBAR_TRAMITE;
                // url = url.Replace("{id}", idSolicitudTramite.ToString());
                // var response = await _httpClient.PostAsJsonAsync(url, new { });
                // return response.IsSuccessStatusCode;

                // TODO: Implementar cuando esté disponible el endpoint
                return false;
            }
            catch (Exception ex)
            {
                // TODO: Manejar excepción
                throw;
            }
        }

        public async Task<bool> RechazarTramite(long idSolicitudTramite, string motivo)
        {
            try
            {
                // var url = TramiteEndpoints.BASE + TramiteEndpoints.RECHAZAR_TRAMITE;
                // url = url.Replace("{id}", idSolicitudTramite.ToString());
                // var request = new { Motivo = motivo };
                // var response = await _httpClient.PostAsJsonAsync(url, request);
                // return response.IsSuccessStatusCode;

                // TODO: Implementar cuando esté disponible el endpoint
                return false;
            }
            catch (Exception ex)
            {
                // TODO: Manejar excepción
                throw;
            }
        }

        public async Task<ResumenDashboardFuncionarioDTO> ObtenerResumenDashboard()
        {
            try
            {
                // var url = TramiteEndpoints.BASE + TramiteEndpoints.RESUMEN_DASHBOARD;
                // var response = await _httpClient.GetFromJsonAsync<ResumenDashboardFuncionarioDTO>(url);
                // return response!;

                // TODO: Implementar cuando esté disponible el endpoint
                return new ResumenDashboardFuncionarioDTO();
            }
            catch (Exception ex)
            {
                // TODO: Manejar excepción
                throw;
            }
        }

        #endregion
    }
}
