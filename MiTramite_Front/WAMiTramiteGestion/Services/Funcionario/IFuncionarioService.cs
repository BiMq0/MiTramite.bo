using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
namespace WAMiTramiteGestion.Services
{
    public interface IFuncionarioService
    {
        Task<FuncionarioAccesosDTO> IniciarSesion(FuncionarioLoginDTO funcionarioLoginDTO);
        void CerrarSesion();
        FuncionarioAccesosDTO? FuncionarioActual { get; set; }
    }
}