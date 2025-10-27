using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.EstadoTramiteRep
{
    public interface IEstadoTramiteRepository
    {
        Task<IEnumerable<EstadoTramite>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<EstadoTramite?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(EstadoTramite entity, CancellationToken cancellationToken = default);
        void Update(EstadoTramite entity);
        void Remove(EstadoTramite entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
