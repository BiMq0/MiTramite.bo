using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RolPermisoRep
{
    public interface IRolPermisoRepository
    {
        Task<IEnumerable<RolPermiso>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RolPermiso?> GetByIdAsync(int idRol, int idPermiso, CancellationToken cancellationToken = default);
        Task AddAsync(RolPermiso entity, CancellationToken cancellationToken = default);
        void Update(RolPermiso entity);
        void Remove(RolPermiso entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
