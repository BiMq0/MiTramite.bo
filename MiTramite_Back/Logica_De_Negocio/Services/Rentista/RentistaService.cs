using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.RentistaRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.RentistaSvc
{
    public class RentistaService : IRentistaService
    {
        private readonly IRentistaRepository _repository;

        public RentistaService(IRentistaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Rentista>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<Rentista?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(id, cancellationToken);

        public async Task AddAsync(Rentista entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Rentista entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Rentista entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
