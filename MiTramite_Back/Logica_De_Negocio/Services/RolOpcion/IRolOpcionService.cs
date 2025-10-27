using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.RolOpcionSvc
{
    public interface IRolOpcionService
    {
        Task<IEnumerable<RolOpcion>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RolOpcion?> GetByIdAsync(int idRol, int idOpcion, CancellationToken cancellationToken = default);
        Task AddAsync(RolOpcion entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(RolOpcion entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(RolOpcion entity, CancellationToken cancellationToken = default);
    }
}
