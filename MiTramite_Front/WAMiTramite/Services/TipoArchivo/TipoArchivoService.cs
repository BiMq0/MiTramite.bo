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

    public async Task<List<TipoArchivoParaSubirDTO>> OBtenerTiposDeDocumentoParaSubir()
    {
        string url = TipoArchivoEndpoints.BASE + TipoArchivoEndpoints.OBTENER_TODOS;
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
}
