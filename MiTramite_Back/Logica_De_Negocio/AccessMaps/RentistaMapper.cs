using MiTramite_Back.Logica_De_Negocio.Services.RentistaSvc;
using MiTramite_Shared.DTOs.RentistaDTOs;
using MiTramite_Shared.Endpoints;
using Microsoft.AspNetCore.Mvc;
using MiTramite_Back.Middleware.Tokens;

namespace MiTramite_Back;

public static class RentistaMapper
{
    public static void Map(this WebApplication app)
    {
        var rentistas = app.MapGroup(RentistaEndpoints.BASE);

        rentistas.MapPost(RentistaEndpoints.SIGNUP, async (RentistaSignupDTO rentistaNuevo, IRentistaService service) =>
        {
            return await service.RegistrarNuevoRentista(rentistaNuevo);
        });

        rentistas.MapPost(RentistaEndpoints.LOGIN, async (RentistaLoginDTO rentistaLogin, IRentistaService service, HttpContext http, ITokenService tokenService) =>
        {
            var resultado = await service.IniciarSesionRentista(rentistaLogin);
            if (resultado != null)
            {
                var token = await tokenService.GenerarTokenRentista(resultado);
                http.Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(2)
                });
                return Results.Ok(resultado);
            }
            return Results.Unauthorized();
        });

        // TODO: Agregar más endpoints relacionados con Rentista aquí y crear los servicios correspondientes
    }
}
