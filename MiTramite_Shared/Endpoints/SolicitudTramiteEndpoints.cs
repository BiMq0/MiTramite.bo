namespace MiTramite_Shared.Endpoints;

public static class SolicitudTramiteEndpoints
{
    public const string BASE = "solicitud-tramite";
    public const string CREAR_SOLICITUD_TRAMITE = "/crear-solicitud-tramite/{idRentista}";
    public const string OBTENER_TRAMITES_POR_RENTISTA = "/obtener-tramites/{idRentista}";
    public const string OBTENER_TRAMITE_POR_ID = "/obtener-tramite/{idTramite}";
    public const string COMPLETAR_TRAMITES = "/completar-tramite/{idTramite}";
    public const string RECHAZAR_TRAMITE = "/rechazar-tramite/{idTramite}";

}
