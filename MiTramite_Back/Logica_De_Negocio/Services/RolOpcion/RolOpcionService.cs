using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Back.Acceso_A_Datos.Repositories.RolOpcionRep;

namespace MiTramite_Back.Logica_De_Negocio.Services.RolOpcionSvc
{
    public class RolOpcionService : IRolOpcionService
    {
        private readonly IRolOpcionRepository _repository;

        public RolOpcionService(IRolOpcionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RolOpcion>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<RolOpcion?> GetByIdAsync(int idRol, int idOpcion, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(idRol, idOpcion, cancellationToken);

        public async Task AddAsync(RolOpcion entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(RolOpcion entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(RolOpcion entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
