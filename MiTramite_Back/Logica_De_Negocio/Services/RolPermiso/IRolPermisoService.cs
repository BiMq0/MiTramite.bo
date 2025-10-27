using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.RolPermisoSvc
{
    public interface IRolPermisoService
    {
        Task<IEnumerable<RolPermiso>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RolPermiso?> GetByIdAsync(int idRol, int idPermiso, CancellationToken cancellationToken = default);
        Task AddAsync(RolPermiso entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(RolPermiso entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(RolPermiso entity, CancellationToken cancellationToken = default);
    }
}
