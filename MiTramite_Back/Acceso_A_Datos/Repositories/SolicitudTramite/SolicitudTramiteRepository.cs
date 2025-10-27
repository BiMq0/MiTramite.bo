using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramiteRep
{
    public class SolicitudTramiteRepository : ISolicitudTramiteRepository
    {
        private readonly MiTramiteDbContext _context;

        public SolicitudTramiteRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SolicitudTramite>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.SolicitudTramites.ToListAsync(cancellationToken);

        public async Task<SolicitudTramite?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _context.SolicitudTramites.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(SolicitudTramite entity, CancellationToken cancellationToken = default)
        {
            await _context.SolicitudTramites.AddAsync(entity, cancellationToken);
        }

        public void Update(SolicitudTramite entity)
        {
            _context.SolicitudTramites.Update(entity);
        }

        public void Remove(SolicitudTramite entity)
        {
            _context.SolicitudTramites.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
