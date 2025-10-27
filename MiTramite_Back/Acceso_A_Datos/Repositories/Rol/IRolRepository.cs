using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RolRep
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Rol?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Rol entity, CancellationToken cancellationToken = default);
        void Update(Rol entity);
        void Remove(Rol entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
