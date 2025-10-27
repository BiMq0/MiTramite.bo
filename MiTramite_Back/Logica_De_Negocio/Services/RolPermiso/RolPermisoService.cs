using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Back.Acceso_A_Datos.Repositories.RolPermisoRep;

namespace MiTramite_Back.Logica_De_Negocio.Services.RolPermisoSvc
{
    public class RolPermisoService : IRolPermisoService
    {
        private readonly IRolPermisoRepository _repository;

        public RolPermisoService(IRolPermisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RolPermiso>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<RolPermiso?> GetByIdAsync(int idRol, int idPermiso, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(idRol, idPermiso, cancellationToken);

        public async Task AddAsync(RolPermiso entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(RolPermiso entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(RolPermiso entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
