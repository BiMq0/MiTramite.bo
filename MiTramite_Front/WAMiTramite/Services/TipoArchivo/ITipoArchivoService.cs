using MiTramite_Shared.DTOs.TipoArchivoDTOs;

namespace WAMiTramite.Services;

public interface ITipoArchivoService
{
    Task<List<TipoArchivoParaSubirDTO>> OBtenerTiposDeDocumentoParaSubir();
}
