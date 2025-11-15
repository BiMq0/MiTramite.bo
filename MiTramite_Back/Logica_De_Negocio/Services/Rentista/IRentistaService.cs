using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.RentistaDTOs;


namespace MiTramite_Back.Logica_De_Negocio.Services.RentistaSvc
{
    public interface IRentistaService
    {
        Task<RentistaCurrentDataDTO> IniciarSesionRentista(RentistaLoginDTO rentistaLogin, CancellationToken cancellationToken = default);
        Task<bool> RegistrarNuevoRentista(RentistaSignupDTO rentistaSignup, CancellationToken cancellationToken = default);
    }
}
