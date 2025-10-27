using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RolOpcionRep
{
    public class RolOpcionRepository : IRolOpcionRepository
    {
        private readonly MiTramiteDbContext _context;

        public RolOpcionRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolOpcion>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.RolOpciones.ToListAsync(cancellationToken);

        public async Task<RolOpcion?> GetByIdAsync(int idRol, int idOpcion, CancellationToken cancellationToken = default)
            => await _context.RolOpciones.FindAsync(new object[] { idRol, idOpcion }, cancellationToken);

        public async Task AddAsync(RolOpcion entity, CancellationToken cancellationToken = default)
        {
            await _context.RolOpciones.AddAsync(entity, cancellationToken);
        }

        public void Update(RolOpcion entity)
        {
            _context.RolOpciones.Update(entity);
        }

        public void Remove(RolOpcion entity)
        {
            _context.RolOpciones.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
