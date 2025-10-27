using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.IncumplimientoRep
{
    public class IncumplimientoRepository : IIncumplimientoRepository
    {
        private readonly MiTramiteDbContext _context;

        public IncumplimientoRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Incumplimiento>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Incumplimientos.ToListAsync(cancellationToken);

        public async Task<Incumplimiento?> GetByIdAsync(long idSolicitudTramite, long idFuncionario, CancellationToken cancellationToken = default)
            => await _context.Incumplimientos.FindAsync(new object[] { idSolicitudTramite, idFuncionario }, cancellationToken);

        public async Task AddAsync(Incumplimiento entity, CancellationToken cancellationToken = default)
        {
            await _context.Incumplimientos.AddAsync(entity, cancellationToken);
        }

        public void Update(Incumplimiento entity)
        {
            _context.Incumplimientos.Update(entity);
        }

        public void Remove(Incumplimiento entity)
        {
            _context.Incumplimientos.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
