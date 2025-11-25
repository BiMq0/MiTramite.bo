using MiTramite_Shared.DTOs.TipoTramiteDTOs;

namespace WAMiTramite.Services;

public interface ITipoTramiteService
{
    Task<List<TipoTramiteDTO>> ObtenerTramitesDisponibles();
    Task<TipoTramiteDTO?> ObtenerTramitePorId(int idTipoTramite);
}
