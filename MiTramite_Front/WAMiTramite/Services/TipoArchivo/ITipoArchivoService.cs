using MiTramite_Shared.DTOs.TipoArchivoDTOs;

namespace WAMiTramite.Services;

public interface ITipoArchivoService
{
    Task<List<TipoArchivoParaSubirDTO>> ObtenerArchivosRequeridosPorTramite(int idTipoTramite);
    Task<List<TipoArchivoParaSubirDTO>> ObtenerArchivosNoSubidosPorRentista(int idRentista, int idTipoTramite);
}
