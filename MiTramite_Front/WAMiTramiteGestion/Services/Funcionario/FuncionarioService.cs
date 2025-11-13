using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
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
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.LOGIN;
            var response = await _httpClient.PostAsJsonAsync(url, funcionarioLoginDTO);
            var funcionario = await response.Content.ReadFromJsonAsync<FuncionarioAccesosDTO>();
            if (!response.IsSuccessStatusCode || funcionario == null) throw new Exception("Error al iniciar sesión del funcionario.");

            FuncionarioActual = funcionario;
            return funcionario;
        }

        public void CerrarSesion()
        {
            FuncionarioActual = null;
        }
    }
}