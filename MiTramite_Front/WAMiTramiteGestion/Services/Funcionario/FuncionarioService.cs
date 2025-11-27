using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WAMiTramiteGestion.Handlers;

namespace WAMiTramiteGestion.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        public FuncionarioAccesosDTO? FuncionarioActual { get; set; }
        private readonly HttpClient _httpClient;
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly LoginStateService _loginStateService;
        private const string FuncionarioStorageKey = "funcionario-actual";

        public FuncionarioService(
            IHttpClientFactory httpClientFactory,
            ProtectedLocalStorage protectedLocalStorage,
            LoginStateService loginStateService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _protectedLocalStorage = protectedLocalStorage;
            _loginStateService = loginStateService;
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
                    await PersistFuncionarioAsync(funcionario);
                    EnsureLoginState();
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

        public async Task<FuncionarioAccesosDTO?> ObtenerFuncionarioActualAsync()
        {
            if (FuncionarioActual != null)
            {
                EnsureLoginState();
                return FuncionarioActual;
            }

            try
            {
                var storedResult = await _protectedLocalStorage.GetAsync<FuncionarioAccesosDTO>(FuncionarioStorageKey);
                if (storedResult.Success && storedResult.Value != null)
                {
                    FuncionarioActual = storedResult.Value;
                    EnsureLoginState();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"ProtectedLocalStorage no disponible: {ex.Message}");
            }

            return FuncionarioActual;
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

        public async Task CerrarSesionAsync()
        {
            FuncionarioActual = null;
            try
            {
                await _protectedLocalStorage.DeleteAsync(FuncionarioStorageKey);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"No se pudo limpiar la sesión local: {ex.Message}");
            }

            _loginStateService.NotifyLogout();
        }

        private async Task PersistFuncionarioAsync(FuncionarioAccesosDTO funcionario)
        {
            try
            {
                await _protectedLocalStorage.SetAsync(FuncionarioStorageKey, funcionario);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"No se pudo persistir el funcionario: {ex.Message}");
            }
        }

        private void EnsureLoginState()
        {
            if (FuncionarioActual != null && !_loginStateService.EstaAutenticado)
            {
                _loginStateService.NotifyLoginSuccess(FuncionarioActual);
            }
        }
    }
}