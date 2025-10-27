using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<Funcionario>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Funcionario?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Funcionario entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Funcionario entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Funcionario entity, CancellationToken cancellationToken = default);
    }
}
