using MiTramite_Back.Logica_De_Negocio.Services.TramiteSvc;
using Microsoft.AspNetCore.Authorization;

namespace MiTramite_Back.AccessMaps
{
    public static class TipoTramiteMapper
    {
        public static void Map(this WebApplication app)
        {
            var tiposTramite = app.MapGroup("api/tipo-tramite");

            // OBTENER TODOS LOS TIPOS DE TRÁMITE
            tiposTramite.MapGet("/obtener-todos", [Authorize] async (ITipoTramiteService service) =>
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

            // OBTENER POR ID
            tiposTramite.MapGet("/{idTipoTramite:int}", [Authorize] async (int idTipoTramite, ITipoTramiteService service) =>
            {
                try
                {
                    var tipo = await service.ObtenerPorIdAsync(idTipoTramite);
                    return Results.Ok(tipo);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { error = "Tipo de trámite no encontrado" });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            });
        }
    }
}
