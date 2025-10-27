using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep
{
    public interface IArchivoRepository
    {
        Task<IEnumerable<Archivo>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Archivo?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Archivo entity, CancellationToken cancellationToken = default);
        void Update(Archivo entity);
        void Remove(Archivo entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
