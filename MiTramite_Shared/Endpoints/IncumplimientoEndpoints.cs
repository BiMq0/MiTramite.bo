namespace MiTramite_Shared.Endpoints
{
    public static class IncumplimientoEndpoints
    {
        public const string Base = "/api/incumplimientos";
        public const string ObtenerTodos = Base;
        public const string ObtenerPorId = Base + "/{idSolicitudTramite}";
    }
}
