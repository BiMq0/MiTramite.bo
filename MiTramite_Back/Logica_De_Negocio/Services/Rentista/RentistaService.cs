using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.RentistaRep;
using MiTramite_Back.Logica_De_Negocio.Services.EmailSvc;
using MiTramite_Shared.DTOs.RentistaDTOs;


namespace MiTramite_Back.Logica_De_Negocio.Services.RentistaSvc
{
    public class RentistaService : IRentistaService
    {
        private readonly IRentistaRepository _repository;
        private readonly IEmailService _emailService;

        public RentistaService(IRentistaRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<RentistaCurrentDataDTO> IniciarSesionRentista(RentistaLoginDTO rentistaLogin, CancellationToken cancellationToken = default)
        {
            var rentista = await _repository.IniciarSesionRentistaAsync(rentistaLogin, cancellationToken);
            return new RentistaCurrentDataDTO(rentista);
        }

        public async Task<bool> RegistrarNuevoRentista(RentistaSignupDTO rentistaSignup, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[RENTISTA SERVICE] Iniciando registro de nuevo rentista: {rentistaSignup.Nombres} ({rentistaSignup.Correo})");

            rentistaSignup.Password = BCrypt.Net.BCrypt.HashPassword(rentistaSignup.Password);

            if (rentistaSignup.FechaNacimiento.Kind == DateTimeKind.Unspecified)
            {
                rentistaSignup.FechaNacimiento = DateTime.SpecifyKind(rentistaSignup.FechaNacimiento, DateTimeKind.Utc);
            }

            Console.WriteLine($"[RENTISTA SERVICE] Guardando rentista en base de datos...");
            var resultado = await _repository.RegistrarRentistaAsync(rentistaSignup, cancellationToken);

            if (resultado)
            {
                Console.WriteLine($"[RENTISTA SERVICE] ✅ Rentista registrado exitosamente en DB");

                // Enviar correo de bienvenida después del registro exitoso
                if (!string.IsNullOrEmpty(rentistaSignup.Correo) && !string.IsNullOrEmpty(rentistaSignup.Nombres))
                {
                    Console.WriteLine($"[RENTISTA SERVICE] Disparando envío de correo de bienvenida...");
                    try
                    {
                        await _emailService.EnviarBienvenidaRentistaAsync(
                            rentistaSignup.Correo,
                            rentistaSignup.Nombres,
                            cancellationToken
                        );
                        Console.WriteLine($"[RENTISTA SERVICE] ✅ Proceso de bienvenida completado satisfactoriamente");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[RENTISTA SERVICE] ⚠️ Advertencia: Correo de bienvenida falló pero el registro fue exitoso: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"[RENTISTA SERVICE] ⚠️ Correo o nombre vacío, no se envía bienvenida");
                }
            }
            else
            {
                Console.WriteLine($"[RENTISTA SERVICE] ❌ El registro en base de datos falló");
            }

            return resultado;
        }

        // TRÁMITES
        public async Task<bool> CrearSolicitudTramiteAsync(int idRentista, int idTipoTramite, CancellationToken cancellationToken = default)
        {
            return await _repository.CrearSolicitudTramiteAsync(idRentista, idTipoTramite, cancellationToken);
        }
    }
}
