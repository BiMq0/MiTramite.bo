using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.TipoTramiteRep
{
    public interface ITipoTramiteRepository
    {
        Task<IEnumerable<TipoTramite>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TipoTramite?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(TipoTramite entity, CancellationToken cancellationToken = default);
        void Update(TipoTramite entity);
        void Remove(TipoTramite entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
