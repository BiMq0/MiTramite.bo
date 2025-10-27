using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
// using fully-qualified entity types to avoid collision with namespace names
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.OpcionSvc
{
    public interface IOpcionService
    {
        Task<IEnumerable<Opcion>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Opcion?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Opcion entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Opcion entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Opcion entity, CancellationToken cancellationToken = default);
    }
}
