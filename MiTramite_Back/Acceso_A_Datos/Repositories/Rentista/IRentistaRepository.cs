using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RentistaRep
{
    public interface IRentistaRepository
    {
        Task<IEnumerable<Rentista>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Rentista?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Rentista entity, CancellationToken cancellationToken = default);
        void Update(Rentista entity);
        void Remove(Rentista entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
