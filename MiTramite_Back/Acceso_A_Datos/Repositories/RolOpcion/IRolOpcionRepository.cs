using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;


namespace MiTramite_Back.Acceso_A_Datos.Repositories.RolOpcionRep
{
    public interface IRolOpcionRepository
    {
        Task<IEnumerable<RolOpcion>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RolOpcion?> GetByIdAsync(int idRol, int idOpcion, CancellationToken cancellationToken = default);
        Task AddAsync(RolOpcion entity, CancellationToken cancellationToken = default);
        void Update(RolOpcion entity);
        void Remove(RolOpcion entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
