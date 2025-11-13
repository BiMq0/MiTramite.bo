using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.RentistaDTOs;


// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RentistaRep
{
    public interface IRentistaRepository
    {
        Task<bool> RegistrarRentistaAsync(RentistaSignupDTO rentistaSignup, CancellationToken cancellationToken = default);
        Task<Rentista> IniciarSesionRentistaAsync(RentistaLoginDTO rentistaLogin, CancellationToken cancellationToken = default);
    }
}
