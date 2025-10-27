using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.IncumplimientoRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.IncumplimientoSvc
{
    public class IncumplimientoService : IIncumplimientoService
    {
        private readonly IIncumplimientoRepository _repository;

        public IncumplimientoService(IIncumplimientoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Incumplimiento>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        public async Task<Incumplimiento?> GetByIdAsync(long idSolicitudTramite, long idFuncionario, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(idSolicitudTramite, idFuncionario, cancellationToken);

        public async Task AddAsync(Incumplimiento entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Incumplimiento entity, CancellationToken cancellationToken = default)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Incumplimiento entity, CancellationToken cancellationToken = default)
        {
            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
