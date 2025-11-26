using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;
using MiTramite_Back.Logica_De_Negocio.Services.SolicitudTramitesSvc;

namespace MiTramite_Back.Logica_De_Negocio.AccessMaps
{
    public static class SolicitusTramiteMapper
    {
        public static void Map(WebApplication app)
        {
            var solicitudTramite = app.MapGroup(SolicitudTramiteEndpoints.BASE)
                .RequireAuthorization();

            // CREAR SOLICITUD DE TRAMITE
            solicitudTramite.MapPost(SolicitudTramiteEndpoints.CREAR_SOLICITUD_TRAMITE, async (SolicitudTramiteNuevoDTO solicitudNueva, ISolicitudTramiteService service) =>
            {
                try
                {
                    var resultado = await service.CrearSolicitudTramiteAsync(solicitudNueva);
                    return resultado
                        ? Results.Created("", new { message = "Solicitud de trámite creada exitosamente" })
                        : Results.BadRequest(new { error = "No se pudo crear la solicitud" });
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("CrearSolicitudTramite");

            // OBTENER TRÁMITES POR RENTISTA
            solicitudTramite.MapGet(SolicitudTramiteEndpoints.OBTENER_TRAMITES_POR_RENTISTA, async (long idRentista, ISolicitudTramiteService service) =>
            {
                try
                {
                    var tramites = await service.ObtenerTramitesPorRentistaAsync(idRentista);
                    if (!tramites.Any())
                        return Results.NotFound(new { message = "No hay trámites para este rentista" });

                    return Results.Ok(tramites);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("ObtenerTramitesPorRentista");

            // OBTENER TRÁMITE POR ID
            solicitudTramite.MapGet(SolicitudTramiteEndpoints.OBTENER_TRAMITE_POR_ID, async (long idTramite, ISolicitudTramiteService service) =>
            {
                try
                {
                    var tramite = await service.ObtenerTramitePorIdAsync(idTramite);
                    if (tramite == null)
                        return Results.NotFound(new { error = "Trámite no encontrado" });

                    return Results.Ok(tramite);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("ObtenerTramitePorId");

            // COMPLETAR TRAMITE
            solicitudTramite.MapPost(SolicitudTramiteEndpoints.COMPLETAR_TRAMITES, async (long idTramite, ISolicitudTramiteService service) =>
            {
                try
                {
                    var resultado = await service.CompletarTramiteAsync(idTramite);
                    return resultado
                        ? Results.Ok(new { message = "Trámite completado exitosamente" })
                        : Results.NotFound(new { error = "Trámite no encontrado" });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("CompletarTramite");

            // RECHAZAR TRAMITE
            solicitudTramite.MapPost(SolicitudTramiteEndpoints.RECHAZAR_TRAMITE, async (long idTramite, string motivo, ISolicitudTramiteService service) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(motivo))
                        return Results.BadRequest(new { error = "El motivo del rechazo es requerido" });

                    var resultado = await service.RechazarTramiteAsync(idTramite, motivo);
                    return resultado
                        ? Results.Ok(new { message = "Trámite rechazado exitosamente" })
                        : Results.NotFound(new { error = "Trámite no encontrado" });
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("RechazarTramite");

            // OBTENER TRÁMITES POR FUNCIONARIO
            solicitudTramite.MapGet(SolicitudTramiteEndpoints.OBTENER_TRAMITES_POR_FUNCIONARIO, async (long idFuncionario, ISolicitudTramiteService service) =>
            {
                try
                {
                    var tramites = await service.ObtenerTramitesPorFuncionarioAsync(idFuncionario);
                    return Results.Ok(tramites);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("ObtenerTramitesPorFuncionario");

            // OBTENER TODOS LOS TRÁMITES (GERENTE)
            solicitudTramite.MapGet(SolicitudTramiteEndpoints.OBTENER_TODOS, async (ISolicitudTramiteService service) =>
            {
                try
                {
                    var tramites = await service.ObtenerTodosLosTramitesAsync();
                    return Results.Ok(tramites);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: ex.Message, statusCode: 500);
                }
            }).WithName("ObtenerTodosLosTramites");
        }
    }
}