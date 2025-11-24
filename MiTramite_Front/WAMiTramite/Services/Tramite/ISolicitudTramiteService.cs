using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

namespace WAMiTramite.Services;

public interface ISolicitudTramiteService
{
    Task<bool> CrearSolicitud(SolicitudTramiteNuevoDTO solicitud);
    Task<List<object>> ObtenerSolicitudesDelRentista(int idRentista);
}
