using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep;
// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _repository;

        public ArchivoService(IArchivoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Archivo>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<Archivo?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(id, cancellationToken);

        public async Task AddAsync(Archivo entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Archivo entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Archivo entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
