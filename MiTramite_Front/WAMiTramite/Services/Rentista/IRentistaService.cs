using MiTramite_Shared.DTOs.RentistaDTOs;
namespace WAMiTramite.Services;

public interface IRentistaService
{
    RentistaCurrentDataDTO rentistaCurrentData { get; set; }
    Task<RentistaCurrentDataDTO> IniciarSesionRentista(RentistaLoginDTO rentistaLoginDTO);
    Task<bool> RegistrarRentista(RentistaSignupDTO rentistaSignupDTO);
    Task<bool> CrearSolicitudTramite(int idRentista, int idTipoTramite);
    Task<List<dynamic>> ObtenerTramites(int idRentista);
    Task<dynamic?> ObtenerTramitePorId(int idRentista, int idTramite);
    Task<List<dynamic>> ObtenerDocumentos(int idRentista);
    Task<bool> SubirDocumento(int idRentista, List<byte[]> archivos);
    Task<bool> EliminarDocumento(int idRentista, int idDocumento);
}
