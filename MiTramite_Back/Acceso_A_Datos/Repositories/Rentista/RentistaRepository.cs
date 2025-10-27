using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RentistaRep
{
    public class RentistaRepository : IRentistaRepository
    {
        private readonly MiTramiteDbContext _context;

        public RentistaRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rentista>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Rentistas.ToListAsync(cancellationToken);

        public async Task<Rentista?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _context.Rentistas.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(Rentista entity, CancellationToken cancellationToken = default)
        {
            await _context.Rentistas.AddAsync(entity, cancellationToken);
        }

        public void Update(Rentista entity)
        {
            _context.Rentistas.Update(entity);
        }

        public void Remove(Rentista entity)
        {
            _context.Rentistas.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
