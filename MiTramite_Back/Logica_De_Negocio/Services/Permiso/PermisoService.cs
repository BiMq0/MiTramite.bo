using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Back.Acceso_A_Datos.Repositories.PermisoRep;

namespace MiTramite_Back.Logica_De_Negocio.Services.PermisoSvc
{
    public class PermisoService : IPermisoService
    {
        private readonly IPermisoRepository _repository;

        public PermisoService(IPermisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<Permiso?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(id, cancellationToken);

        public async Task AddAsync(Permiso entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Permiso entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Permiso entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
