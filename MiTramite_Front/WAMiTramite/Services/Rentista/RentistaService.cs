using MiTramite_Shared.DTOs.RentistaDTOs;
using MiTramite_Shared.Endpoints;

namespace WAMiTramite.Services;

public class RentistaService : IRentistaService
{
    private readonly HttpClient _client;
    public RentistaCurrentDataDTO rentistaCurrentData { get; set; }
    public RentistaService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ApiClient");
    }

    public async Task<RentistaCurrentDataDTO> IniciarSesionRentista(RentistaLoginDTO rentistaLoginDTO)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.LOGIN;
        Console.WriteLine("URL de inicio de sesion: " + _client.BaseAddress + url);
        var resultado = await _client.PostAsJsonAsync(url, rentistaLoginDTO);
        var rentista = await resultado.Content.ReadFromJsonAsync<RentistaCurrentDataDTO>();
        rentistaCurrentData = rentista!;
        return rentistaCurrentData;
    }

    public async Task<bool> RegistrarRentista(RentistaSignupDTO rentistaSignupDTO)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.SIGNUP;
        Console.WriteLine("URL de registro: " + _client.BaseAddress + url);
        var resultado = await _client.PostAsJsonAsync(url, rentistaSignupDTO);
        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> CrearSolicitudTramite(int idRentista, int idTipoTramite)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.CREAR_SOLICITUD_TRAMITE.Replace("{idRentista}", idRentista.ToString());
        var requestBody = new { idTipoTramite };
        Console.WriteLine("URL de crear solicitud: " + _client.BaseAddress + url);
        var resultado = await _client.PostAsJsonAsync(url, requestBody);
        return resultado.IsSuccessStatusCode;
    }

    public async Task<List<dynamic>> ObtenerTramites(int idRentista)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.OBTENER_TRAMITES.Replace("{idRentista}", idRentista.ToString());
        Console.WriteLine("URL de obtener trámites: " + _client.BaseAddress + url);
        var resultado = await _client.GetAsync(url);
        if (resultado.IsSuccessStatusCode)
        {
            var tramites = await resultado.Content.ReadFromJsonAsync<List<dynamic>>();
            return tramites ?? new List<dynamic>();
        }
        return new List<dynamic>();
    }

    public async Task<dynamic?> ObtenerTramitePorId(int idRentista, int idTramite)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.OBTENER_TRAMITE_POR_ID
            .Replace("{idRentista}", idRentista.ToString())
            .Replace("{idTramite}", idTramite.ToString());
        Console.WriteLine("URL de obtener trámite por ID: " + _client.BaseAddress + url);
        var resultado = await _client.GetAsync(url);
        if (resultado.IsSuccessStatusCode)
        {
            return await resultado.Content.ReadFromJsonAsync<dynamic>();
        }
        return null;
    }

    public async Task<List<dynamic>> ObtenerDocumentos(int idRentista)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.OBTENER_DOCUMENTOS
            .Replace("{idRentista}", idRentista.ToString())
            .Replace("{idDocumento}", "");
        Console.WriteLine("URL de obtener documentos: " + _client.BaseAddress + url);
        var resultado = await _client.GetAsync(url);
        if (resultado.IsSuccessStatusCode)
        {
            var documentos = await resultado.Content.ReadFromJsonAsync<List<dynamic>>();
            return documentos ?? new List<dynamic>();
        }
        return new List<dynamic>();
    }

    public async Task<bool> SubirDocumento(int idRentista, List<byte[]> archivos)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.SUBIR_DOCUMENTO.Replace("{idRentista}", idRentista.ToString());
        Console.WriteLine("URL de subir documento: " + _client.BaseAddress + url);

        using (var content = new MultipartFormDataContent())
        {
            for (int i = 0; i < archivos.Count; i++)
            {
                var fileContent = new ByteArrayContent(archivos[i]);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                content.Add(fileContent, "archivos", $"documento_{i}.pdf");
            }

            var resultado = await _client.PostAsync(url, content);
            return resultado.IsSuccessStatusCode;
        }
    }

    public async Task<bool> EliminarDocumento(int idRentista, int idDocumento)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.ELIMINAR_DOCUMENTO
            .Replace("{idRentista}", idRentista.ToString())
            .Replace("{idDocumento}", idDocumento.ToString());
        Console.WriteLine("URL de eliminar documento: " + _client.BaseAddress + url);
        var resultado = await _client.DeleteAsync(url);
        return resultado.IsSuccessStatusCode;
    }
}
