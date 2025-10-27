using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.PermisoRep
{
    public interface IPermisoRepository
    {
        Task<IEnumerable<Permiso>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Permiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Permiso entity, CancellationToken cancellationToken = default);
        void Update(Permiso entity);
        void Remove(Permiso entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
