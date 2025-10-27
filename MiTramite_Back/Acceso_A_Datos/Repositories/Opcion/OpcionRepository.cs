using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.OpcionRep
{
    public class OpcionRepository : IOpcionRepository
    {
        private readonly MiTramiteDbContext _context;
        public OpcionRepository(MiTramiteDbContext context) => _context = context;

        public async Task<IEnumerable<Opcion>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Opciones.ToListAsync(cancellationToken);

        public async Task<Opcion?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.Opciones.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(Opcion entity, CancellationToken cancellationToken = default)
            => await _context.Opciones.AddAsync(entity, cancellationToken);

        public void Update(Opcion entity) => _context.Opciones.Update(entity);

        public void Remove(Opcion entity) => _context.Opciones.Remove(entity);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
