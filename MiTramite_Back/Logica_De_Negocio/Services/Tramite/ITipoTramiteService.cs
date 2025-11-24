using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.TipoTramiteDTOs;

namespace MiTramite_Back.Logica_De_Negocio.Services.TramiteSvc
{
    public interface ITipoTramiteService
    {
        Task<List<TipoTramiteDTO>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
        Task<TipoTramiteDTO> ObtenerPorIdAsync(int idTipoTramite, CancellationToken cancellationToken = default);
    }
}
