using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.FuncionarioDTOs;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.FuncionarioRep
{
    public interface IFuncionarioRepository
    {
        Task<FuncionarioAccesosDTO> IniciarSesionFuncionarioAsync(FuncionarioLoginDTO funcionarioLogin, CancellationToken cancellationToken = default);
    }
}
