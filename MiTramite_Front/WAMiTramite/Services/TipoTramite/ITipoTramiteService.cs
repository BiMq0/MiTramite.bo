using MiTramite_Shared.DTOs.TipoTramiteDTOs;

namespace WAMiTramite.Services;

public interface ITipoTramiteService
{
    Task<List<TipoTramiteDTO>> ObtenerTramitesDisponibles(int idRentista);
    Task<TipoTramiteDTO?> ObtenerTramitePorId(int idTipoTramite);
}
