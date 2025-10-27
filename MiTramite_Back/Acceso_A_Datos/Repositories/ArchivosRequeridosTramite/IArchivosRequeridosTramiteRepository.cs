using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
namespace MiTramite_Back.Acceso_A_Datos.Repositories.ArchivosRequeridosTramiteRep
{
    public interface IArchivosRequeridosTramiteRepository
    {
        Task<IEnumerable<ArchivosRequeridosTramite>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ArchivosRequeridosTramite?> GetByIdAsync(int idTipoTramite, int idTipoArchivo, CancellationToken cancellationToken = default);
        Task AddAsync(ArchivosRequeridosTramite entity, CancellationToken cancellationToken = default);
        void Update(ArchivosRequeridosTramite entity);
        void Remove(ArchivosRequeridosTramite entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
