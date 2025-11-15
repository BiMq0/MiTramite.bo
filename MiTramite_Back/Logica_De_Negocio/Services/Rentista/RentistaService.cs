using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.RentistaRep;
using MiTramite_Shared.DTOs.RentistaDTOs;


namespace MiTramite_Back.Logica_De_Negocio.Services.RentistaSvc
{
    public class RentistaService : IRentistaService
    {
        private readonly IRentistaRepository _repository;

        public RentistaService(IRentistaRepository repository)
        {
            _repository = repository;
        }

        public async Task<RentistaCurrentDataDTO> IniciarSesionRentista(RentistaLoginDTO rentistaLogin, CancellationToken cancellationToken = default)
        {

            var rentista = await _repository.IniciarSesionRentistaAsync(rentistaLogin, cancellationToken);
            return new RentistaCurrentDataDTO(rentista);
        }

        public async Task<bool> RegistrarNuevoRentista(RentistaSignupDTO rentistaSignup, CancellationToken cancellationToken = default)
        {
            rentistaSignup.Password = BCrypt.Net.BCrypt.HashPassword(rentistaSignup.Password);

            // Convertir a UTC para PostgreSQL - especificar que es UTC
            if (rentistaSignup.FechaNacimiento.Kind == DateTimeKind.Unspecified)
            {
                rentistaSignup.FechaNacimiento = DateTime.SpecifyKind(rentistaSignup.FechaNacimiento, DateTimeKind.Utc);
            }

            return await _repository.RegistrarRentistaAsync(rentistaSignup, cancellationToken);
        }
    }
}
