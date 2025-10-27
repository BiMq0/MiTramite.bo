using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;


namespace MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramiteRep
{
    public interface ISolicitudTramiteRepository
    {
        Task<IEnumerable<SolicitudTramite>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SolicitudTramite?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(SolicitudTramite entity, CancellationToken cancellationToken = default);
        void Update(SolicitudTramite entity);
        void Remove(SolicitudTramite entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
