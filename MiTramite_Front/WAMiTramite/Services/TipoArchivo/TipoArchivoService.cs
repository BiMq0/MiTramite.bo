using MiTramite_Shared.DTOs.TipoArchivoDTOs;
using MiTramite_Shared.Endpoints;

namespace WAMiTramite.Services;

public class TipoArchivoService : ITipoArchivoService
{
    private readonly HttpClient _client;

    public TipoArchivoService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ApiClient");
    }

    public async Task<List<TipoArchivoParaSubirDTO>> ObtenerArchivosRequeridosPorTramite(int idTipoTramite)
    {
        string url = $"tipoArchivo/tramite/{idTipoTramite}";
        Console.WriteLine("URL de archivos requeridos: " + _client.BaseAddress + url);
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                var archivos = await resultado.Content.ReadFromJsonAsync<List<TipoArchivoParaSubirDTO>>();
                return archivos ?? new List<TipoArchivoParaSubirDTO>();
            }
            return new List<TipoArchivoParaSubirDTO>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener archivos requeridos: {ex.Message}");
            return new List<TipoArchivoParaSubirDTO>();
        }
    }

    public async Task<List<TipoArchivoParaSubirDTO>> ObtenerArchivosNoSubidosPorRentista(int idRentista, int idTipoTramite)
    {
        string url = $"tipoArchivo/no-subidos/{idRentista}/{idTipoTramite}";
        Console.WriteLine("URL de archivos no subidos: " + _client.BaseAddress + url);
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                var archivos = await resultado.Content.ReadFromJsonAsync<List<TipoArchivoParaSubirDTO>>();
                return archivos ?? new List<TipoArchivoParaSubirDTO>();
            }
            return new List<TipoArchivoParaSubirDTO>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener archivos no subidos: {ex.Message}");
            return new List<TipoArchivoParaSubirDTO>();
        }
    }
}
