using Microsoft.AspNetCore.Mvc;
using MiTramite_Back.Logica_De_Negocio.Services.Reportes;
using MiTramite_Shared.Endpoints;

namespace MiTramite_Back.Logica_De_Negocio.AccessMaps
{
    public static class ReporteMapper
    {
        public static void Map(WebApplication app)
        {
            app.MapGet(ReporteEndpoints.GetDashboardData, async ([FromServices] IReporteService service, [FromQuery] int year) =>
            {
                if (year == 0) year = DateTime.Now.Year;
                var data = await service.GetDashboardDataAsync(year);
                return Results.Ok(data);
            })
            .WithTags("Reportes");
        }
    }
}
