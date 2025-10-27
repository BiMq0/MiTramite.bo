using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.TipoArchivoRep;
using MiTramite_Domain.Entities;


namespace MiTramite_Back.Logica_De_Negocio.Services.TipoArchivoSvc
{
    public class TipoArchivoService : ITipoArchivoService
    {
        private readonly ITipoArchivoRepository _repository;

        public TipoArchivoService(ITipoArchivoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TipoArchivo>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<TipoArchivo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(id, cancellationToken);

        public async Task AddAsync(TipoArchivo entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TipoArchivo entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TipoArchivo entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
