using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.TipoTramiteRep
{
    public class TipoTramiteRepository : ITipoTramiteRepository
    {
        private readonly MiTramiteDbContext _context;

        public TipoTramiteRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoTramite>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.TipoTramites.ToListAsync(cancellationToken);

        public async Task<TipoTramite?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.TipoTramites.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(TipoTramite entity, CancellationToken cancellationToken = default)
        {
            await _context.TipoTramites.AddAsync(entity, cancellationToken);
        }

        public void Update(TipoTramite entity)
        {
            _context.TipoTramites.Update(entity);
        }

        public void Remove(TipoTramite entity)
        {
            _context.TipoTramites.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
