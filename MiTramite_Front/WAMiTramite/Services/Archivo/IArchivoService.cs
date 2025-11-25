using MiTramite_Shared.DTOs.ArchivoDTOs;

namespace WAMiTramite.Services;

public interface IArchivoService
{
    Task<bool> SubirArchivo(ArchivoNuevoDTO archivo);
    Task<List<ArchivoRegistroDTO>> ObtenerArchivosDelRentista(int idRentista);
    Task<bool> EliminarArchivo(long idArchivo);
}
