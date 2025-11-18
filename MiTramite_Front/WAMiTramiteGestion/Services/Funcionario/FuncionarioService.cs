using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using System.Net;
namespace WAMiTramiteGestion.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly HttpClient _httpClient;
        public FuncionarioAccesosDTO? FuncionarioActual { get; set; }
        public FuncionarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FuncionarioAccesosDTO> IniciarSesion(FuncionarioLoginDTO funcionarioLoginDTO)
        {
            try
            {
                var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.LOGIN;
                var response = await _httpClient.PostAsJsonAsync(url, funcionarioLoginDTO);
                var funcionario = await response.Content.ReadFromJsonAsync<FuncionarioAccesosDTO>();
                if (response.IsSuccessStatusCode)
                {
                    FuncionarioActual = funcionario;
                    return FuncionarioActual!;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Credenciales inválidas. Por favor, verifique su usuario y contraseña.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new UnauthorizedAccessException("El usuario no fue encontrado. Por favor, verifique sus datos.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al iniciar sesión: {response.StatusCode}, Detalles: {errorContent}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Error de conexión al servidor. Por favor, intente nuevamente más tarde.", httpEx);
            }

        }

        public void CerrarSesion()
        {
            FuncionarioActual = null;
        }
    }
}