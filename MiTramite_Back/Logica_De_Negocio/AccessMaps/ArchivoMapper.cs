using MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc;
using Microsoft.AspNetCore.Authorization;

namespace MiTramite_Back.AccessMaps
{
    public static class ArchivoMapper
    {
        public static void Map(this WebApplication app)
        {
            var archivos = app.MapGroup("api/archivo");

            // OBTENER DOCUMENTOS DEL RENTISTA
            archivos.MapGet("/documentos/{idRentista:int}", [Authorize] async (int idRentista, IArchivoService service) =>
            {
                try
                {
                    var documentos = await service.ObtenerDocumentosRentistaAsync(idRentista);
                    return Results.Ok(documentos);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            });

            // SUBIR DOCUMENTO
            archivos.MapPost("/subir/{idRentista:int}", [Authorize] async (int idRentista, int idTipoArchivo, string nombreArchivo, IFormFile archivo, IArchivoService service) =>
            {
                try
                {
                    // Validaciones
                    if (archivo == null || archivo.Length == 0)
                        return Results.BadRequest(new { error = "El archivo es requerido" });

                    // Solo PDF
                    var extension = Path.GetExtension(archivo.FileName).ToLower();
                    if (extension != ".pdf")
                        return Results.BadRequest(new { error = "Solo se permiten archivos PDF" });

                    // Máximo 5MB
                    const long maxSizeBytes = 5 * 1024 * 1024;
                    if (archivo.Length > maxSizeBytes)
                        return Results.BadRequest(new { error = "El archivo excede el tamaño máximo de 5MB" });

                    // Convertir a bytes
                    using (var memoryStream = new MemoryStream())
                    {
                        await archivo.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();

                        var resultado = await service.SubirDocumentoAsync(idRentista, idTipoArchivo, nombreArchivo, contenido);
                        return resultado
                            ? Results.Created("", new { message = "Documento subido exitosamente" })
                            : Results.BadRequest(new { error = "No se pudo subir el documento" });
                    }
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            });

            // ELIMINAR DOCUMENTO
            archivos.MapDelete("/eliminar/{idRentista:int}/{idDocumento:long}", [Authorize] async (int idRentista, long idDocumento, IArchivoService service) =>
            {
                try
                {
                    var resultado = await service.EliminarDocumentoAsync(idRentista, idDocumento);
                    return resultado
                        ? Results.NoContent()
                        : Results.NotFound(new { error = "Documento no encontrado" });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            });

            // VERIFICAR SI EXISTE DOCUMENTO
            archivos.MapGet("/existe/{idRentista:int}/{idTipoArchivo:int}", [Authorize] async (int idRentista, int idTipoArchivo, IArchivoService service) =>
            {
                try
                {
                    var existe = await service.ExisteDocumentoAsync(idRentista, idTipoArchivo);
                    return Results.Ok(new { existe });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            });
        }
    }
}
