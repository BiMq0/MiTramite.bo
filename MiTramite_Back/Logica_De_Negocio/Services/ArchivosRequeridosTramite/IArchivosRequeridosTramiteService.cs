using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivosRequeridosTramiteSvc
{
    public interface IArchivosRequeridosTramiteService
    {
        Task<IEnumerable<ArchivosRequeridosTramite>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ArchivosRequeridosTramite?> GetByIdAsync(int idTipoTramite, int idTipoArchivo, CancellationToken cancellationToken = default);
        Task AddAsync(ArchivosRequeridosTramite entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(ArchivosRequeridosTramite entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(ArchivosRequeridosTramite entity, CancellationToken cancellationToken = default);
    }
}
