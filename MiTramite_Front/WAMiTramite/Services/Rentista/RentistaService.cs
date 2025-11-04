using MiTramite_Shared.DTOs.RentistaDTOs;
using MiTramite_Shared.Endpoints;
using System.Net.Http.Json;
using System.Net.Http;

namespace WAMiTramite.Services;

public class RentistaService : IRentistaService
{
    private readonly HttpClient _client;
    public RentistaService(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> IniciarSesionRentista(RentistaLoginDTO rentistaLoginDTO)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.LOGIN;
        Console.WriteLine("URL de inicio de sesion: " + _client.BaseAddress + url);
        var resultado = await _client.PostAsJsonAsync(url, rentistaLoginDTO);
        return resultado.IsSuccessStatusCode;
    }

    public async Task<bool> RegistrarRentista(RentistaSignupDTO rentistaSignupDTO)
    {
        string url = RentistaEndpoints.BASE + RentistaEndpoints.SIGNUP;
        Console.WriteLine("URL de registro: " + _client.BaseAddress + url);
        var resultado = await _client.PostAsJsonAsync(url, rentistaSignupDTO);
        return resultado.IsSuccessStatusCode;
    }
}
