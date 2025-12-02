using System.Net.Http.Json;
using MiTramite_Shared.DTOs.ArchivoDTOs;
using MiTramite_Shared.DTOs.ArchivosRequeridosTramite;
using MiTramite_Shared.Endpoints;

namespace WAMiTramiteGestion.Services.Archivo;

public class ArchivoService : IArchivoService
{
    private readonly HttpClient _client;

    public ArchivoService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ApiClient");
    }

    public async Task<List<ArchivoRegistroDTO>> ObtenerArchivosDelRentista(long idRentista)
    {
        string url = ArchivoEndpoints.BASE + ArchivoEndpoints.OBTENER_DOCUMENTOS_POR_RENTISTA.Replace("{idRentista}", idRentista.ToString());
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                var datos = await resultado.Content.ReadFromJsonAsync<List<ArchivoRegistroDTO>>();
                return datos ?? new List<ArchivoRegistroDTO>();
            }
            return new List<ArchivoRegistroDTO>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<ArchivoRegistroDTO>();
        }
    }

    public async Task<List<ArchivosRequeridosTramiteDTO>> ObtenerArchivosRequeridos(int idTipoTramite)
    {
        string url = ArchivoEndpoints.BASE + ArchivoEndpoints.OBTENER_ARCHIVOS_REQUERIDOS.Replace("{idTipoTramite}", idTipoTramite.ToString());
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                var datos = await resultado.Content.ReadFromJsonAsync<List<ArchivosRequeridosTramiteDTO>>();
                return datos ?? new List<ArchivosRequeridosTramiteDTO>();
            }
            return new List<ArchivosRequeridosTramiteDTO>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<ArchivosRequeridosTramiteDTO>();
        }
    }
}
