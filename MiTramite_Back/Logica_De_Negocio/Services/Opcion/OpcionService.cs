using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
// using fully-qualified entity types to avoid collision with namespace names
using MiTramite_Back.Acceso_A_Datos.Repositories.OpcionRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.OpcionSvc
{
    public class OpcionService : IOpcionService
    {
        private readonly IOpcionRepository _repository;

        public OpcionService(IOpcionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Opcion>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Opcion?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task AddAsync(Opcion entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Opcion entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Opcion entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
