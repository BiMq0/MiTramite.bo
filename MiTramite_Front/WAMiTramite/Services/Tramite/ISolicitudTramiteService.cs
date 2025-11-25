using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

namespace WAMiTramite.Services;

public interface ISolicitudTramiteService
{
    Task<bool> CrearSolicitud(SolicitudTramiteNuevoDTO solicitud);
    Task<List<SolicitudTramiteRegistroDTO>> ObtenerSolicitudesDelRentista(int idRentista);
    Task<SolicitudTramiteRegistroDTO> ObtenerSolicitudDeTramitePorId(int idSolicitudTramite);
}
