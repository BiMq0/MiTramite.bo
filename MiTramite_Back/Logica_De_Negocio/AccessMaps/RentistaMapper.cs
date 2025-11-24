using MiTramite_Back.Logica_De_Negocio.Services.RentistaSvc;
using MiTramite_Shared.DTOs.RentistaDTOs;
using MiTramite_Shared.Endpoints;
using MiTramite_Back.Middleware.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace MiTramite_Back;

public static class RentistaMapper
{
    public static void Map(this WebApplication app)
    {
        var rentistas = app.MapGroup(RentistaEndpoints.BASE);

        // AUTENTICACIÓN
        rentistas.MapPost(RentistaEndpoints.SIGNUP, async (RentistaSignupDTO rentistaNuevo, IRentistaService service) =>
        {
            try
            {
                var resultado = await service.RegistrarNuevoRentista(rentistaNuevo);
                return resultado
                    ? Results.Created($"{RentistaEndpoints.BASE}/signup", new { message = "Rentista registrado exitosamente" })
                    : Results.BadRequest(new { error = "No se pudo registrar el rentista" });
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

        rentistas.MapPost(RentistaEndpoints.LOGIN, async (RentistaLoginDTO rentistaLogin, IRentistaService service, ITokenService tokenService, HttpContext httpContext) =>
        {
            try
            {
                var resultado = await service.IniciarSesionRentista(rentistaLogin);
                if (resultado == null)
                    return Results.Unauthorized();

                var token = await tokenService.GenerarTokenRentista(resultado);
                httpContext.Response.Cookies.Append("token", token, tokenService.ConfigurarCookie());

                return Results.Ok(new { message = "Login exitoso", data = resultado });
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound(new { error = "Rentista no encontrado" });
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Unauthorized();
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        });
    }
}
