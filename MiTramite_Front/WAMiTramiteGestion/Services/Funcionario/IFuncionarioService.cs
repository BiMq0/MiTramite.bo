using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.FuncionarioDTOs;

namespace WAMiTramiteGestion.Services
{
    public interface IFuncionarioService
    {
        FuncionarioAccesosDTO? FuncionarioActual { get; set; }
        Task<FuncionarioAccesosDTO> IniciarSesion(FuncionarioLoginDTO funcionarioLoginDTO);
        Task<FuncionarioAccesosDTO?> ObtenerFuncionarioActualAsync();
        Task CerrarSesionAsync();

        #region Métodos de Gerente
        Task<List<FuncionarioRegistroDTO>> ObtenerTodosLosFuncionarios();
        Task<FuncionarioEditDTO> ObtenerFuncionarioPorId(long id);
        Task<bool> RegistrarNuevoFuncionario(FuncionarioNuevoDTO funcionarioNuevo);
        Task<bool> ActualizarFuncionario(FuncionarioEditDTO funcionarioEdit);
        #endregion

        #region Métodos de Funcionario Estándar

        #endregion
    }
}