using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using MiTramite_Shared.DTOs.RentistaDTOs;

namespace MiTramite_Back.Middleware.Authentication
{
    public interface IAuthService
    {
        Task<string> GenerarTokenFuncionario(FuncionarioAccesosDTO funcionarioDto);
        Task<string> GenerarTokenRentista(RentistaCurrentDataDTO funcionarioDto);
    }
}