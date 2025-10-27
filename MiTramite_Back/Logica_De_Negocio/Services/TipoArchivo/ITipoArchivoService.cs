using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.TipoArchivoRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.TipoArchivoSvc
{
    public interface ITipoArchivoService
    {
        Task<IEnumerable<TipoArchivo>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TipoArchivo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(TipoArchivo entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TipoArchivo entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TipoArchivo entity, CancellationToken cancellationToken = default);
    }
}
