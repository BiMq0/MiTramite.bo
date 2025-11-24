using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.TipoTramiteDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.TramiteRep
{
    public interface ITipoTramiteRepository
    {
        Task<List<TipoTramiteDTO>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
        Task<TipoTramiteDTO> ObtenerPorIdAsync(int idTipoTramite, CancellationToken cancellationToken = default);
    }
}
