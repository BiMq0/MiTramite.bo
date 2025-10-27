using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RolRep
{
    public class RolRepository : IRolRepository
    {
        private readonly MiTramiteDbContext _context;

        public RolRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Roles.ToListAsync(cancellationToken);

        public async Task<Rol?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.Roles.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(Rol entity, CancellationToken cancellationToken = default)
        {
            await _context.Roles.AddAsync(entity, cancellationToken);
        }

        public void Update(Rol entity)
        {
            _context.Roles.Update(entity);
        }

        public void Remove(Rol entity)
        {
            _context.Roles.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
