using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using System.Net;
using System.Text.Json;
using WAMiTramiteGestion.Services.Base;

namespace WAMiTramiteGestion.Services
{
    public class FuncionarioService : BaseApiService, IFuncionarioService
    {
        public FuncionarioAccesosDTO? FuncionarioActual { get; set; }

        public FuncionarioService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<FuncionarioAccesosDTO> IniciarSesion(FuncionarioLoginDTO funcionarioLoginDTO)
        {
            try
            {
                var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.LOGIN;
                var funcionario = await PostAsync<FuncionarioAccesosDTO>(url, funcionarioLoginDTO);

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

        public async Task<List<FuncionarioRegistroDTO>> ObtenerTodosLosFuncionarios()
        {
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.OBTENER_TODOS;
            var funcionarios = await GetAsync<List<FuncionarioRegistroDTO>>(url);
            return funcionarios ?? new List<FuncionarioRegistroDTO>();
        }

        public async Task<FuncionarioRegistroDTO?> ObtenerFuncionarioPorId(long id)
        {
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.OBTENER_POR_ID + id;
            return await GetAsync<FuncionarioRegistroDTO>(url);
        }

        public async Task<bool> RegistrarNuevoFuncionario(FuncionarioNuevoDTO funcionarioNuevo)
        {
            var url = FuncionarioEndpoints.BASE + FuncionarioEndpoints.REGISTER;
            return await PostAsync(url, funcionarioNuevo);
        }

        public void CerrarSesion()
        {
            FuncionarioActual = null;
        }
    }
}