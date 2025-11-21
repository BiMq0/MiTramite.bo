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
        void CerrarSesion();
        Task<List<FuncionarioRegistroDTO>> ObtenerTodosLosFuncionarios();
        Task<FuncionarioRegistroDTO?> ObtenerFuncionarioPorId(long id);
        Task<bool> RegistrarNuevoFuncionario(FuncionarioNuevoDTO funcionarioNuevo);
    }
}