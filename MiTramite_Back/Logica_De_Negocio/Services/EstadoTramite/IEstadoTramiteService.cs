using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.EstadoTramiteSvc
{
    public interface IEstadoTramiteService
    {
        Task<IEnumerable<EstadoTramite>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<EstadoTramite?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(EstadoTramite entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(EstadoTramite entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(EstadoTramite entity, CancellationToken cancellationToken = default);
    }
}
