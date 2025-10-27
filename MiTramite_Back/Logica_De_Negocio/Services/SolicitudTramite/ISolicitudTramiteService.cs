using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramiteRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.SolicitudTramiteSvc
{
    public interface ISolicitudTramiteService
    {
        Task<IEnumerable<SolicitudTramite>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SolicitudTramite?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(SolicitudTramite entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(SolicitudTramite entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(SolicitudTramite entity, CancellationToken cancellationToken = default);
    }
}
