using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc;
using MiTramite_Back.Middleware.Authentication;
using System.Security.Claims;
using MiTramite_Back.Middleware.Tokens;

namespace MiTramite_Back.AccessMaps;

public static class FuncionarioMapper
{
    public static void Map(this WebApplication app)
    {
        var funcionarios = app.MapGroup(FuncionarioEndpoints.BASE);

        funcionarios.MapPost(FuncionarioEndpoints.LOGIN, async (FuncionarioLoginDTO funcionarioLogin, IFuncionarioService service, IAuthService authService, HttpContext http) =>
        {
            var dto = await service.IniciarSesionFuncionario(funcionarioLogin);
            if (dto == null) return Results.Unauthorized();

            var token = await authService.GenerarTokenFuncionario(dto);


            http.Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(2) // Cookie válida por 2 horas, igual que el token
            });

            return Results.Ok(dto);
        });

        funcionarios.MapGet(FuncionarioEndpoints.GET_BY_ID, async (IFuncionarioService service, ITokenService tokenService, HttpContext http) =>
        {
            var token = http.Request.Cookies["token"];
            if (string.IsNullOrEmpty(token))
            {
                return Results.Unauthorized();
            }

            return Results.Ok();
        });
    }
}
