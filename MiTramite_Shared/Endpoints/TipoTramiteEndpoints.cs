using System.Data;

namespace MiTramite_Shared.Endpoints;

public static class TipoTramiteEndpoints
{
    public const string BASE = "tipo-tramite";
    public const string OBTENER_TODOS = "/obtener-todos";
    public const string OBTENER_POR_ID = "/obtener-por-id/{idTipoTramite}";
}
