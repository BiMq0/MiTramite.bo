using MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc;
using Microsoft.AspNetCore.Authorization;
using MiTramite_Shared.Endpoints;

namespace MiTramite_Back.AccessMaps
{
    public static class TipoArchivoMapper
    {
        public static void Map(this WebApplication app)
        {
            var tiposArchivo = app.MapGroup(TipoArchivoEndpoints.BASE)
                .RequireAuthorization(new AuthorizeAttribute
                {
                    AuthenticationSchemes = "Bearer",
                });

            tiposArchivo.MapGet(TipoArchivoEndpoints.OBTENER_TODOS, async (ITipoArchivoService service) =>
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
        }
    }
}
