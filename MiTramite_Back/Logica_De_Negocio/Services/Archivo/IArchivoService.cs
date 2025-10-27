using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc
{
    public interface IArchivoService
    {
        Task<IEnumerable<Archivo>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Archivo?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Archivo entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Archivo entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Archivo entity, CancellationToken cancellationToken = default);
    }
}
