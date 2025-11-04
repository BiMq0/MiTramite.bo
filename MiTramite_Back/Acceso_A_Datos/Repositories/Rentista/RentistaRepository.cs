using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.RentistaDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RentistaRep
{
    public class RentistaRepository : IRentistaRepository
    {
        private readonly MiTramiteDbContext _context;

        public RentistaRepository(MiTramiteDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegistrarRentistaAsync(RentistaSignupDTO rentistaSignup, CancellationToken cancellationToken = default)
        {
            var nuevoRentista = new Rentista
            {
                CI = rentistaSignup.CI!,
                Nombres = rentistaSignup.Nombres!,
                ApellidoPaterno = rentistaSignup.ApellidoPaterno!,
                ApellidoMaterno = rentistaSignup.ApellidoMaterno!,
                FechaNacimiento = rentistaSignup.FechaNacimiento,
                Correo = rentistaSignup.Correo!,
                PasswordHash = rentistaSignup.Password!,
            };

            await _context.Rentistas.AddAsync(nuevoRentista, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
        public async Task<bool> IniciarSesionRentistaAsync(RentistaLoginDTO rentistaLogin, CancellationToken cancellationToken = default)
        {
            var rentista = await _context.Rentistas
                .FirstOrDefaultAsync(r => r.Correo == rentistaLogin.Correo, cancellationToken);
            Console.WriteLine(rentista?.Correo);
            Console.WriteLine(rentista?.PasswordHash);
            if (rentista == null)
            {
                return false;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(rentistaLogin.Password, rentista.PasswordHash);

            return isPasswordValid;
        }
    }
}

