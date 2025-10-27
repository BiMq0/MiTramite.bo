using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.ArchivosRequeridosTramiteRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivosRequeridosTramiteSvc
{
    public class ArchivosRequeridosTramiteService : IArchivosRequeridosTramiteService
    {
        private readonly IArchivosRequeridosTramiteRepository _repository;

        public ArchivosRequeridosTramiteService(IArchivosRequeridosTramiteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ArchivosRequeridosTramite>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<ArchivosRequeridosTramite?> GetByIdAsync(int idTipoTramite, int idTipoArchivo, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(idTipoTramite, idTipoArchivo, cancellationToken);

        public async Task AddAsync(ArchivosRequeridosTramite entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ArchivosRequeridosTramite entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ArchivosRequeridosTramite entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
