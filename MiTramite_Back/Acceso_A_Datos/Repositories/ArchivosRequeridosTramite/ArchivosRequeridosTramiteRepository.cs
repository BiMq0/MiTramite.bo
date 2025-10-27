using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.ArchivosRequeridosTramiteRep
{
    public class ArchivosRequeridosTramiteRepository : IArchivosRequeridosTramiteRepository
    {
        private readonly MiTramiteDbContext _context;

        public ArchivosRequeridosTramiteRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArchivosRequeridosTramite>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.ArchivosRequeridosTramites.ToListAsync(cancellationToken);

        public async Task<ArchivosRequeridosTramite?> GetByIdAsync(int idTipoTramite, int idTipoArchivo, CancellationToken cancellationToken = default)
            => await _context.ArchivosRequeridosTramites.FindAsync(new object[] { idTipoTramite, idTipoArchivo }, cancellationToken);

        public async Task AddAsync(ArchivosRequeridosTramite entity, CancellationToken cancellationToken = default)
        {
            await _context.ArchivosRequeridosTramites.AddAsync(entity, cancellationToken);
        }

        public void Update(ArchivosRequeridosTramite entity)
        {
            _context.ArchivosRequeridosTramites.Update(entity);
        }

        public void Remove(ArchivosRequeridosTramite entity)
        {
            _context.ArchivosRequeridosTramites.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
