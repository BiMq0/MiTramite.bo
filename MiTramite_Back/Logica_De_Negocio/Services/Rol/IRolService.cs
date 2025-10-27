using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.RolSvc
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Rol?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Rol entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Rol entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Rol entity, CancellationToken cancellationToken = default);
    }
}
