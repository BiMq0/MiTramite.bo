using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Back.Middleware.Tokens;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.FuncionarioDTOs;


namespace MiTramite_Back.Acceso_A_Datos.Repositories.FuncionarioRep
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly MiTramiteDbContext _context;

        public FuncionarioRepository(MiTramiteDbContext context, ITokenService tokenService)
        {
            _context = context;
        }
        public async Task<FuncionarioAccesosDTO> IniciarSesionFuncionarioAsync(FuncionarioLoginDTO funcionarioLogin, CancellationToken cancellationToken = default)
        {
            var funcionario = await _context.Funcionarios
                    .Include(f => f.Rol)
                        .ThenInclude(r => r!.RolPermisos)
                            .ThenInclude(rp => rp!.Permiso)
                    .Include(f => f.Rol)
                        .ThenInclude(r => r!.RolOpciones)
                            .ThenInclude(ro => ro!.Opcion)
                .FirstOrDefaultAsync(f => f.CodigoFuncionario == funcionarioLogin.CodigoFuncionario);



            if (funcionario == null)
            {
                throw new KeyNotFoundException("Funcionario no encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(funcionarioLogin.Password, funcionario.PasswordHash))
            {
                throw new UnauthorizedAccessException("Correo o contraseña incorrecta.");
            }

            var funcionarioToReturn = new FuncionarioAccesosDTO(funcionario);
            return funcionarioToReturn;
        }

        public async Task<List<FuncionarioRegistroDTO>> ObtenerTodosLosFuncionariosAsync(CancellationToken cancellationToken = default)
        {
            var funcionarios = await _context.Funcionarios.ToListAsync(cancellationToken);
            var funcionarioDTOs = funcionarios.Select(f => new FuncionarioRegistroDTO(f)).ToList();
            return funcionarioDTOs;
        }
        public async Task<FuncionarioEditDTO> ObtenerFuncionarioPorIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.IdFuncionario == id, cancellationToken);

            if (funcionario == null)
            {
                throw new KeyNotFoundException("Funcionario no encontrado.");
            }

            var funcionarioEditDTO = new FuncionarioEditDTO(funcionario);
            return funcionarioEditDTO;
        }
        public async Task<bool> CrearFuncionarioAsync(FuncionarioNuevoDTO funcionarioCreate, CancellationToken cancellationToken = default)
        {
            var funcionarioNuevo = new Funcionario
            {
                CodigoFuncionario = "FNC-" + Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper(),
                Nombres = funcionarioCreate.Nombres!,
                ApellidoPaterno = funcionarioCreate.ApellidoPaterno!,
                ApellidoMaterno = funcionarioCreate.ApellidoMaterno!,
                Correo = funcionarioCreate.Correo!,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(funcionarioCreate.Password!),
                IdRol = 1 // Funcionarios por defecto, puesto que no existen más roles que gerentes para crear funcionarios
            };
            _context.Funcionarios.Add(funcionarioNuevo);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
