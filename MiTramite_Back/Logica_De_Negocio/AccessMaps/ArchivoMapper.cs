using MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc;
using Microsoft.AspNetCore.Authorization;
using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.ArchivoDTOs;

namespace MiTramite_Back.AccessMaps
{
    public static class ArchivoMapper
    {
        public static void Map(this WebApplication app)
        {
            var archivos = app.MapGroup(ArchivoEndpoints.BASE).RequireAuthorization();

            archivos.MapGet(ArchivoEndpoints.OBTENER_DOCUMENTOS_POR_RENTISTA, async (int idRentista, IArchivoService service) =>
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
            }).WithName("ObtenerDocumentos");

            archivos.MapPost(ArchivoEndpoints.SUBIR_DOCUMENTO, async (ArchivoNuevoDTO archivoDto, IArchivoService service) =>
            {
                try
                {
                    var resultado = await service.SubirDocumentoAsync(
                        archivoDto.IdRentista,
                        archivoDto.IdTipoArchivo,
                        archivoDto.Nombre,
                        archivoDto.Contenido
                    );

                    return resultado
                        ? Results.Created("", new { message = "Documento subido exitosamente" })
                        : Results.BadRequest(new { error = "No se pudo subir el documento" });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("SubirDocumento");

            archivos.MapDelete(ArchivoEndpoints.ELIMINAR_DOCUMENTO, async (long idDocumento, IArchivoService service) =>
            {
                try
                {
                    var resultado = await service.EliminarDocumentoAsync(0, idDocumento);
                    return resultado
                        ? Results.NoContent()
                        : Results.NotFound(new { error = "Documento no encontrado" });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("EliminarDocumento");

            archivos.MapGet(ArchivoEndpoints.OBTENER_ARCHIVOS_REQUERIDOS, async (int idTipoTramite, IArchivoService service) =>
            {
                try
                {
                    var requeridos = await service.ObtenerArchivosRequeridosAsync(idTipoTramite);
                    return Results.Ok(requeridos);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("ObtenerArchivosRequeridos");
        }
    }
}
