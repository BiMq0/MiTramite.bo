using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.IncumplimientoSvc
{
    public interface IIncumplimientoService
    {
        Task<IEnumerable<Incumplimiento>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Incumplimiento?> GetByIdAsync(long idSolicitudTramite, long idFuncionario, CancellationToken cancellationToken = default);
        Task AddAsync(Incumplimiento entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Incumplimiento entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Incumplimiento entity, CancellationToken cancellationToken = default);
    }
}
