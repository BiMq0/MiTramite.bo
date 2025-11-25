
using MiTramite_Shared.DTOs.ArchivoDTOs;
using MiTramite_Shared.Endpoints;

namespace WAMiTramite.Services;

public class ArchivoService : IArchivoService
{
    private readonly HttpClient _client;

    public ArchivoService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ApiClient");
    }

    public async Task<List<ArchivoRegistroDTO>> ObtenerArchivosDelRentista(int idRentista)
    {
        string url = ArchivoEndpoints.BASE + ArchivoEndpoints.OBTENER_DOCUMENTOS_POR_RENTISTA.Replace("{idRentista}", idRentista.ToString());
        Console.WriteLine("URL: " + _client.BaseAddress + url);
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

    public async Task<bool> SubirArchivo(ArchivoNuevoDTO archivo)
    {
        string url = ArchivoEndpoints.BASE + ArchivoEndpoints.SUBIR_DOCUMENTO;
        Console.WriteLine("URL: " + _client.BaseAddress + url);
        try
        {
            var respuesta = await _client.PostAsJsonAsync(url, archivo);
            return respuesta.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> EliminarArchivo(long idArchivo)
    {
        string url = ArchivoEndpoints.BASE + ArchivoEndpoints.ELIMINAR_DOCUMENTO
            .Replace("{idDocumento}", idArchivo.ToString());
        Console.WriteLine("URL: " + _client.BaseAddress + url);
        try
        {
            var respuesta = await _client.DeleteAsync(url);
            return respuesta.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}