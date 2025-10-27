using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.PermisoRep
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly MiTramiteDbContext _context;

        public PermisoRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Permisos.ToListAsync(cancellationToken);

        public async Task<Permiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.Permisos.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(Permiso entity, CancellationToken cancellationToken = default)
        {
            await _context.Permisos.AddAsync(entity, cancellationToken);
        }

        public void Update(Permiso entity)
        {
            _context.Permisos.Update(entity);
        }

        public void Remove(Permiso entity)
        {
            _context.Permisos.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
