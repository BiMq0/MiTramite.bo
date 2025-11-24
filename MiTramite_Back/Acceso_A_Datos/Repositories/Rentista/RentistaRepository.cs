using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.RentistaDTOs;
using Npgsql;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RentistaRep
{
    public class RentistaRepository : IRentistaRepository
    {
        private readonly MiTramiteDbContext _context;

        public RentistaRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        // AUTENTICACIÓN
        public async Task<bool> RegistrarRentistaAsync(RentistaSignupDTO rentistaSignup, CancellationToken cancellationToken = default)
        {
            try
            {
                var nuevoRentista = new Rentista
                {
                    CI = rentistaSignup.CI!,
                    Nombres = rentistaSignup.Nombres!,
                    ApellidoPaterno = rentistaSignup.ApellidoPaterno!,
                    ApellidoMaterno = rentistaSignup.ApellidoMaterno!,
                    Telefono = rentistaSignup.Telefono!,
                    FechaNacimiento = rentistaSignup.FechaNacimiento,
                    Correo = rentistaSignup.Correo!,
                    PasswordHash = rentistaSignup.Password!,
                };

                await _context.Rentistas.AddAsync(nuevoRentista, cancellationToken);
                var result = await _context.SaveChangesAsync(cancellationToken);

                return result > 0;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
            {
                if (pgEx.SqlState == "23505") // Unique violation
                    throw new InvalidOperationException($"El CI '{rentistaSignup.CI}' ya está registrado", ex);
                throw;
            }
        }

        public async Task<Rentista> IniciarSesionRentistaAsync(RentistaLoginDTO rentistaLogin, CancellationToken cancellationToken = default)
        {
            var rentista = await _context.Rentistas
                .FirstOrDefaultAsync(r => r.Correo == rentistaLogin.Correo, cancellationToken);

            if (rentista == null)
            {
                throw new KeyNotFoundException("Credenciales inválidas.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(rentistaLogin.Password, rentista.PasswordHash);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas.");
            }
            return rentista;
        }

        // TRÁMITES
        public async Task<bool> CrearSolicitudTramiteAsync(int idRentista, int idTipoTramite, CancellationToken cancellationToken = default)
        {
            // TODO: Implementar - Crear nueva SolicitudTramite
            throw new NotImplementedException();
        }
    }
}

