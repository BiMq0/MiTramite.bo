
using MiTramite_Shared.DTOs.ArchivoDTOs;

namespace WAMiTramite.Services;

public class ArchivoService : IArchivoService
{
    private readonly HttpClient _client;

    public ArchivoService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ApiClient");
    }

    public async Task<bool> SubirArchivo(ArchivoNuevoDTO archivo)
    {
        string url = "archivo/nuevo";
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

    public async Task<List<ArchivoRegistroDTO>> ObtenerArchivosDelRentista(int idRentista)
    {
        string url = $"archivo/rentista/{idRentista}";
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

    public async Task<bool> EliminarArchivo(int idRentista, long idArchivo)
    {
        string url = $"archivo/{idRentista}/{idArchivo}";
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