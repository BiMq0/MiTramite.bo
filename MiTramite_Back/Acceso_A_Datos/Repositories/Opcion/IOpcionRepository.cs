using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.OpcionRep
{
    public interface IOpcionRepository
    {
        Task<IEnumerable<Opcion>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Opcion?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Opcion entity, CancellationToken cancellationToken = default);
        void Update(Opcion entity);
        void Remove(Opcion entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
