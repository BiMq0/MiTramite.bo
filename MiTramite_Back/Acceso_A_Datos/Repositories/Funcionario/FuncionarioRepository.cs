using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.FuncionarioRep
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly MiTramiteDbContext _context;

        public FuncionarioRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Funcionarios.ToListAsync(cancellationToken);

        public async Task<Funcionario?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _context.Funcionarios.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(Funcionario entity, CancellationToken cancellationToken = default)
        {
            await _context.Funcionarios.AddAsync(entity, cancellationToken);
        }

        public void Update(Funcionario entity)
        {
            _context.Funcionarios.Update(entity);
        }

        public void Remove(Funcionario entity)
        {
            _context.Funcionarios.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
