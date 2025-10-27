using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.EstadoTramiteRep;
using MiTramite_Domain.Entities;
// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Logica_De_Negocio.Services.EstadoTramiteSvc
{
    public class EstadoTramiteService : IEstadoTramiteService
    {
        private readonly IEstadoTramiteRepository _repository;
        public EstadoTramiteService(IEstadoTramiteRepository repository) => _repository = repository;

        public async Task<IEnumerable<EstadoTramite>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<EstadoTramite?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(id, cancellationToken);

        public async Task AddAsync(EstadoTramite entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(EstadoTramite entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(EstadoTramite entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
