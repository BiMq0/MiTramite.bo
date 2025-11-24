using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using System.Net;
using System.Text.Json;

namespace WAMiTramiteGestion.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        public FuncionarioAccesosDTO? FuncionarioActual { get; set; }
        private readonly HttpClient _httpClient;

        public FuncionarioService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<FuncionarioAccesosDTO> IniciarSesion(FuncionarioLoginDTO funcionarioLoginDTO)
        {
            try
            {
                var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.LOGIN;
                var response = await _httpClient.PostAsJsonAsync(url, funcionarioLoginDTO);
                var funcionario = await response.Content.ReadFromJsonAsync<FuncionarioAccesosDTO>();

                if (funcionario != null)
                {
                    FuncionarioActual = funcionario;
                    return FuncionarioActual;
                }

                throw new Exception("Error al procesar la respuesta del login.");
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas. Por favor, verifique su usuario y contraseña.");
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Error de conexión al servidor. Por favor, intente nuevamente más tarde.", httpEx);
            }
        }

        #region Métodos de Gerente

        public async Task<List<FuncionarioRegistroDTO>> ObtenerTodosLosFuncionarios()
        {
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.OBTENER_TODOS;
            var response = await _httpClient.GetFromJsonAsync<List<FuncionarioRegistroDTO>>(url);

            return response ?? new List<FuncionarioRegistroDTO>();
        }

        public async Task<FuncionarioEditDTO> ObtenerFuncionarioPorId(long id)
        {
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.OBTENER_POR_ID;

            url = url.Replace("{id}", id.ToString());
            var response = await _httpClient.GetFromJsonAsync<FuncionarioEditDTO>(url);
            return response!;
        }

        public async Task<bool> RegistrarNuevoFuncionario(FuncionarioNuevoDTO funcionarioNuevo)
        {
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.REGISTER;
            var response = await _httpClient.PostAsJsonAsync(url, funcionarioNuevo);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> ActualizarFuncionario(FuncionarioEditDTO funcionarioEdit)
        {
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.ACTUALIZAR_DATOS;
            url = url.Replace("{id}", funcionarioEdit.IdFuncionario.ToString());
            var response = await _httpClient.PutAsJsonAsync(url, funcionarioEdit);
            return response.IsSuccessStatusCode;
        }
        #endregion

        #region Métodos de Funcionario Estándar


        #endregion
        public void CerrarSesion()
        {
            FuncionarioActual = null;
        }
    }
}