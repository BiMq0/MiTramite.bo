using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.EstadoTramiteRep
{
    public class EstadoTramiteRepository : IEstadoTramiteRepository
    {
        private readonly MiTramiteDbContext _context;
        public EstadoTramiteRepository(MiTramiteDbContext context) => _context = context;

        public async Task<IEnumerable<EstadoTramite>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.EstadoTramites.ToListAsync(cancellationToken);

        public async Task<EstadoTramite?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.EstadoTramites.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(EstadoTramite entity, CancellationToken cancellationToken = default)
            => await _context.EstadoTramites.AddAsync(entity, cancellationToken);

        public void Update(EstadoTramite entity) => _context.EstadoTramites.Update(entity);

        public void Remove(EstadoTramite entity) => _context.EstadoTramites.Remove(entity);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
