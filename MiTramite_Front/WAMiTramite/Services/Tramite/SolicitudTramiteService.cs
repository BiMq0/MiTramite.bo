using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

namespace WAMiTramite.Services;

public class SolicitudTramiteService : ISolicitudTramiteService
{
    private readonly HttpClient _client;

    public SolicitudTramiteService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ApiClient");
    }

    public async Task<bool> CrearSolicitud(SolicitudTramiteNuevoDTO solicitud)
    {
        string url = "solicitudTramite/nuevo";
        Console.WriteLine("URL: " + _client.BaseAddress + url);
        try
        {
            var respuesta = await _client.PostAsJsonAsync(url, solicitud);
            return respuesta.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public async Task<List<object>> ObtenerSolicitudesDelRentista(int idRentista)
    {
        string url = $"solicitudTramite/rentista/{idRentista}";
        Console.WriteLine("URL: " + _client.BaseAddress + url);
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                var datos = await resultado.Content.ReadFromJsonAsync<List<object>>();
                return datos ?? new List<object>();
            }
            return new List<object>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<object>();
        }
    }
}
