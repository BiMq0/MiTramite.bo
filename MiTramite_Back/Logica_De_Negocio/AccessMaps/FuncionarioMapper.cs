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
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });

            return Results.Ok(dto);
        });
    }
}
