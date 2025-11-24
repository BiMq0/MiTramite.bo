using System.Data;

namespace MiTramite_Shared.Endpoints;

public static class RentistaEndpoints
{
    public const string BASE = "rentista";
    public const string SIGNUP = "/signup";
    public const string LOGIN = "/login";

    public const string CREAR_SOLICITUD_TRAMITE = "/crear-solicitud-tramite/{idRentista}";
    public const string OBTENER_TRAMITES = "/obtener-tramites/{idRentista}";
    public const string OBTENER_TRAMITE_POR_ID = "/obtener-tramite/{idRentista}/{idTramite}";



}
