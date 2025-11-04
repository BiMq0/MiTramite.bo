using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.FuncionarioDTOs;


namespace MiTramite_Back.Acceso_A_Datos.Repositories.FuncionarioRep
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly MiTramiteDbContext _context;

        public FuncionarioRepository(MiTramiteDbContext context)
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


            if (funcionario != null && BCrypt.Net.BCrypt.Verify(funcionarioLogin.Password, funcionario.PasswordHash))
            {
                var funcionarioToReturn = new FuncionarioAccesosDTO(funcionario);
                return funcionarioToReturn;
            }

            return null;
        }
    }
}
