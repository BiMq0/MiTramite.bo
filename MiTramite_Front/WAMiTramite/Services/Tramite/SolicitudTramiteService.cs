using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;
using MiTramite_Shared.Endpoints;

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
        string url = SolicitudTramiteEndpoints.BASE + SolicitudTramiteEndpoints.CREAR_SOLICITUD_TRAMITE.Replace("{idRentista}", solicitud.IdRentista.ToString());

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

    public async Task<List<SolicitudTramiteRegistroDTO>> ObtenerSolicitudesDelRentista(int idRentista)
    {
        string url = SolicitudTramiteEndpoints.BASE + SolicitudTramiteEndpoints.OBTENER_TRAMITES_POR_RENTISTA.Replace("{idRentista}", idRentista.ToString());

        Console.WriteLine("URL: " + _client.BaseAddress + url);
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                var datos = await resultado.Content.ReadFromJsonAsync<List<SolicitudTramiteRegistroDTO>>();
                return datos ?? new List<SolicitudTramiteRegistroDTO>();
            }
            return new List<SolicitudTramiteRegistroDTO>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<SolicitudTramiteRegistroDTO>();
        }
    }

    public async Task<SolicitudTramiteRegistroDTO> ObtenerSolicitudDeTramitePorId(int idSolicitudTramite)
    {
        string url = SolicitudTramiteEndpoints.BASE + SolicitudTramiteEndpoints.OBTENER_TRAMITE_POR_ID.Replace("{idTramite}", idSolicitudTramite.ToString());

        Console.WriteLine("URL: " + _client.BaseAddress + url);
        try
        {
            var resultado = await _client.GetAsync(url);
            if (resultado.IsSuccessStatusCode)
            {
                var dato = await resultado.Content.ReadFromJsonAsync<SolicitudTramiteRegistroDTO>();
                return dato!;
            }
            return null!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }
}
