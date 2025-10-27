using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.FuncionarioRep
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Funcionario?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Funcionario entity, CancellationToken cancellationToken = default);
        void Update(Funcionario entity);
        void Remove(Funcionario entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
