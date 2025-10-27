using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.TipoTramiteRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.TipoTramiteSvc
{
    public interface ITipoTramiteService
    {
        Task<IEnumerable<TipoTramite>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TipoTramite?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(TipoTramite entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TipoTramite entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TipoTramite entity, CancellationToken cancellationToken = default);
    }
}
