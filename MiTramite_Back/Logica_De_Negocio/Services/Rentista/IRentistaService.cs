using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.RentistaSvc
{
    public interface IRentistaService
    {
        Task<IEnumerable<Rentista>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Rentista?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Rentista entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Rentista entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Rentista entity, CancellationToken cancellationToken = default);
    }
}
