using MiTramite_Shared.DTOs.TipoTramiteDTOs;
using MiTramite_Shared.Endpoints;

namespace WAMiTramite.Services;

public class TipoTramiteService : ITipoTramiteService
{
    private readonly HttpClient _client;

    public TipoTramiteService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ApiClient");
    }

    public async Task<List<TipoTramiteDTO>> ObtenerTramitesDisponibles()
    {
        string url = TipoTramiteEndpoints.BASE + TipoTramiteEndpoints.OBTENER_TODOS;
        Console.WriteLine("URL de trámites disponibles: " + _client.BaseAddress + url);
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                var tramites = await resultado.Content.ReadFromJsonAsync<List<TipoTramiteDTO>>();
                return tramites ?? new List<TipoTramiteDTO>();
            }
            return new List<TipoTramiteDTO>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener trámites disponibles: {ex.Message}");
            return new List<TipoTramiteDTO>();
        }
    }

    public async Task<TipoTramiteDTO?> ObtenerTramitePorId(int idTipoTramite)
    {
        string url = TipoTramiteEndpoints.BASE + TipoTramiteEndpoints.OBTENER_POR_ID.Replace("{idTipoTramite}", idTipoTramite.ToString());
        Console.WriteLine("URL de trámite por ID: " + _client.BaseAddress + url);
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                return await resultado.Content.ReadFromJsonAsync<TipoTramiteDTO>();
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener trámite por ID: {ex.Message}");
            return null;
        }
    }
}
