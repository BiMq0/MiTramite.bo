using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
namespace WAMiTramiteGestion.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly HttpClient _httpClient;
        public FuncionarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FuncionarioAccesosDTO> IniciarSesion(FuncionarioLoginDTO funcionarioLoginDTO)
        {
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.LOGIN;
            var response = await _httpClient.PostAsJsonAsync(url, funcionarioLoginDTO);
            var funcionario = await response.Content.ReadFromJsonAsync<FuncionarioAccesosDTO>();
            if (!response.IsSuccessStatusCode || funcionario == null) return null;
            return funcionario;
        }
    }
}