using MiTramite_Back.Logica_De_Negocio.Services.TramiteSvc;
using Microsoft.AspNetCore.Authorization;
using MiTramite_Shared.Endpoints;

namespace MiTramite_Back.AccessMaps
{
    public static class TipoTramiteMapper
    {
        public static void Map(this WebApplication app)
        {
            var tiposTramite = app.MapGroup(TipoTramiteEndpoints.BASE)
                .RequireAuthorization(new AuthorizeAttribute
                {
                    AuthenticationSchemes = "Bearer",
                });

            tiposTramite.MapGet(TipoTramiteEndpoints.OBTENER_TODOS, async (ITipoTramiteService service) =>
            {
                try
                {
                    var tipos = await service.ObtenerTodosAsync();
                    return Results.Ok(tipos);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            });

            tiposTramite.MapGet(TipoTramiteEndpoints.OBTENER_POR_ID, async (int idTipoTramite, ITipoTramiteService service) =>
            {
                try
                {
                    var tipoTramite = await service.ObtenerPorIdAsync(idTipoTramite);
                    return tipoTramite != null
                        ? Results.Ok(tipoTramite)
                        : Results.NotFound(new { error = "Tipo de trámite no encontrado" });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            });
        }
    }
}
