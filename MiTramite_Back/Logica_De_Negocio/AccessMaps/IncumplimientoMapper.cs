using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiTramite_Back.Logica_De_Negocio.Services.Incumplimiento;
using MiTramite_Shared.Endpoints;

namespace MiTramite_Back.Logica_De_Negocio.AccessMaps
{
    public static class IncumplimientoMapper
    {
        public static void Map(WebApplication app)
        {
            app.MapGet(IncumplimientoEndpoints.ObtenerTodos, async (IIncumplimientoService service) =>
            {
                return Results.Ok(await service.ObtenerTodosAsync());
            });

            app.MapGet(IncumplimientoEndpoints.ObtenerPorId, async (long idSolicitudTramite, IIncumplimientoService service) =>
            {
                var result = await service.ObtenerPorIdTramiteAsync(idSolicitudTramite);
                if (result == null) return Results.NotFound();
                return Results.Ok(result);
            });
        }
    }
}
