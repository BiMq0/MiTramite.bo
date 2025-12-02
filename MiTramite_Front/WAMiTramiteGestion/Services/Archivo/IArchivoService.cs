using MiTramite_Shared.DTOs.ArchivoDTOs;
using MiTramite_Shared.DTOs.ArchivosRequeridosTramite;

namespace WAMiTramiteGestion.Services.Archivo;

public interface IArchivoService
{
    Task<List<ArchivoRegistroDTO>> ObtenerArchivosDelRentista(long idRentista);
    Task<List<ArchivosRequeridosTramiteDTO>> ObtenerArchivosRequeridos(int idTipoTramite);
}
