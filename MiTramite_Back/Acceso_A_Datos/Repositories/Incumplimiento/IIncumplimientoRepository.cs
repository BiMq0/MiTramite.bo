using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.IncumplimientoRep
{
    public interface IIncumplimientoRepository
    {
        Task<IEnumerable<Incumplimiento>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Incumplimiento?> GetByIdAsync(long idSolicitudTramite, long idFuncionario, CancellationToken cancellationToken = default);
        Task AddAsync(Incumplimiento entity, CancellationToken cancellationToken = default);
        void Update(Incumplimiento entity);
        void Remove(Incumplimiento entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
