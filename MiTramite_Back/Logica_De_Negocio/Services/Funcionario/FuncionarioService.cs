using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.FuncionarioRep;
using MiTramite_Back.Middleware.Tokens;
using MiTramite_Shared.DTOs.FuncionarioDTOs;


namespace MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _repository;

        public FuncionarioService(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<FuncionarioAccesosDTO> IniciarSesionFuncionario(FuncionarioLoginDTO funcionarioLogin, CancellationToken cancellationToken = default)
        {
            try
            {
                var funcionarioAccesosDTO = await _repository.IniciarSesionFuncionarioAsync(funcionarioLogin, cancellationToken);
                if (funcionarioAccesosDTO == null)
                {
                    throw new UnauthorizedAccessException("Credenciales inválidas.");
                }

                return funcionarioAccesosDTO;
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new KeyNotFoundException("Error al iniciar sesión del funcionario: " + knfEx.Message);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                throw new UnauthorizedAccessException("Error al iniciar sesión del funcionario: " + uaEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar sesión del funcionario: " + ex.Message);
            }
        }

        public async Task<List<FuncionarioRegistroDTO>> ObtenerTodosLosFuncionariosAsync(CancellationToken cancellationToken = default)
        {
            var funcionarios = await _repository.ObtenerTodosLosFuncionariosAsync(cancellationToken);
            return funcionarios;
        }
        public async Task<FuncionarioEditDTO> ObtenerFuncionarioPorIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var funcionarioEditDTO = await _repository.ObtenerFuncionarioPorIdAsync(id, cancellationToken);
            return funcionarioEditDTO;
        }
        public async Task<bool> CrearFuncionarioAsync(FuncionarioNuevoDTO funcionarioCreate, CancellationToken cancellationToken = default)
        {
            bool funcionarioCreado = false;
            try
            {
                funcionarioCreado = await _repository.CrearFuncionarioAsync(funcionarioCreate, cancellationToken);
            }
            catch (DataException dEx)
            {
                throw new DataException($"[DataException] Error al crear funcionario: {dEx.InnerException?.Message} {dEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[Exception] Error al crear funcionario: {ex.Message}");
            }
            return funcionarioCreado;
        }

        public async Task<bool> ActualizarFuncionarioAsync(FuncionarioEditDTO funcionarioEdit, CancellationToken cancellationToken = default)
        {
            bool funcionarioActualizado = false;
            try
            {
                funcionarioActualizado = await _repository.ActualizarFuncionarioAsync(funcionarioEdit, cancellationToken);
            }
            catch (DataException dEx)
            {
                throw new DataException($"[DataException] Error al actualizar funcionario: {dEx.InnerException?.Message} {dEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[Exception] Error al actualizar funcionario: {ex.Message}");
            }
            return funcionarioActualizado;
        }
    }
}
