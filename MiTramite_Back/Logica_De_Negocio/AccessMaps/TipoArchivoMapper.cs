using MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc;
using Microsoft.AspNetCore.Authorization;

namespace MiTramite_Back.AccessMaps
{
    public static class TipoArchivoMapper
    {
        public static void Map(this WebApplication app)
        {
            var tiposArchivo = app.MapGroup("api/tipo-archivo");

            // OBTENER TODOS LOS TIPOS DE ARCHIVO
            tiposArchivo.MapGet("/obtener-todos", [Authorize] async (ITipoArchivoService service) =>
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
            tiposArchivo.MapGet("/{idTipoArchivo:int}", [Authorize] async (int idTipoArchivo, ITipoArchivoService service) =>
            {
                try
                {
                    var tipo = await service.ObtenerPorIdAsync(idTipoArchivo);
                    return Results.Ok(tipo);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { error = "Tipo de archivo no encontrado" });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            });
        }
    }
}
