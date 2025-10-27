using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;


namespace MiTramite_Back.Acceso_A_Datos.Repositories.TipoArchivoRep
{
    public interface ITipoArchivoRepository
    {
        Task<IEnumerable<TipoArchivo>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TipoArchivo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(TipoArchivo entity, CancellationToken cancellationToken = default);
        void Update(TipoArchivo entity);
        void Remove(TipoArchivo entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
