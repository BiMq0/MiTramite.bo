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
}
