using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
namespace MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc
{
    public interface IFuncionarioService
    {
        Task<FuncionarioAccesosDTO> IniciarSesionFuncionario(FuncionarioLoginDTO funcionarioLogin, CancellationToken cancellationToken = default);
    }
}
