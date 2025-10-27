using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Back.Acceso_A_Datos.Repositories.RolRep;

namespace MiTramite_Back.Logica_De_Negocio.Services.RolSvc
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repository;

        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<Rol?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(id, cancellationToken);

        public async Task AddAsync(Rol entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Rol entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Rol entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
