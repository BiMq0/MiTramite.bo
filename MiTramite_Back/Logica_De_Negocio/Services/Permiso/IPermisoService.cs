using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.PermisoSvc
{
    public interface IPermisoService
    {
        Task<IEnumerable<Permiso>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Permiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Permiso entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Permiso entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Permiso entity, CancellationToken cancellationToken = default);
    }
}
