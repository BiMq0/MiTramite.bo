using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RolPermisoRep
{
    public class RolPermisoRepository : IRolPermisoRepository
    {
        private readonly MiTramiteDbContext _context;

        public RolPermisoRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolPermiso>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.RolPermisos.ToListAsync(cancellationToken);

        public async Task<RolPermiso?> GetByIdAsync(int idRol, int idPermiso, CancellationToken cancellationToken = default)
            => await _context.RolPermisos.FindAsync(new object[] { idRol, idPermiso }, cancellationToken);

        public async Task AddAsync(RolPermiso entity, CancellationToken cancellationToken = default)
        {
            await _context.RolPermisos.AddAsync(entity, cancellationToken);
        }

        public void Update(RolPermiso entity)
        {
            _context.RolPermisos.Update(entity);
        }

        public void Remove(RolPermiso entity)
        {
            _context.RolPermisos.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
