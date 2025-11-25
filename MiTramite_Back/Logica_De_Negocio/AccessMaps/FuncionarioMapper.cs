using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc;
using MiTramite_Back.Middleware.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace MiTramite_Back.AccessMaps;

public static class FuncionarioMapper
{
    public static void Map(this WebApplication app)
    {
        var funcionarios = app.MapGroup(FuncionarioEndpoints.BASE)
    .RequireAuthorization(new AuthorizeAttribute
    {
        AuthenticationSchemes = "Bearer",
    });


        funcionarios.MapPost(FuncionarioEndpoints.LOGIN, async (FuncionarioLoginDTO funcionarioLogin, IFuncionarioService service, ITokenService tokenService, HttpContext httpContext) =>
        {
            try
            {
                var funcionarioAccesosDTO = await service.IniciarSesionFuncionario(funcionarioLogin);
                Console.WriteLine("[FUNCIONARIO MAPPER] Datos de acceso del funcionario obtenidos.");
                Console.WriteLine(funcionarioAccesosDTO);

                if (funcionarioAccesosDTO == null)
                    return Results.Unauthorized();

                var token = await tokenService.GenerarTokenFuncionario(funcionarioAccesosDTO);
                Console.WriteLine("[FUNCIONARIO MAPPER] Token generado para funcionario.");
                httpContext.Response.Cookies.Append("token", token, tokenService.ConfigurarCookie());
                Console.WriteLine("[FUNCIONARIO MAPPER] Login de funcionario exitoso.");
                Console.WriteLine(funcionarioAccesosDTO);
                return Results.Ok(funcionarioAccesosDTO);
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound(new { error = "Funcionario no encontrado" });
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Unauthorized();
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        })
        .AllowAnonymous()
        .Produces<FuncionarioAccesosDTO>(StatusCodes.Status200OK);

        funcionarios.MapPost(FuncionarioEndpoints.REGISTER, async (FuncionarioNuevoDTO funcionarioRegister, IFuncionarioService service) =>
        {
            try
            {
                var funcionarioCreado = await service.CrearFuncionarioAsync(funcionarioRegister);
                return funcionarioCreado ? Results.Ok() : Results.BadRequest(new { error = "No se pudo crear el funcionario." });
            }
            catch (ArgumentException argEx)
            {
                return Results.BadRequest(new { error = argEx.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        }).Produces<FuncionarioNuevoDTO>(StatusCodes.Status201Created);

        funcionarios.MapGet(FuncionarioEndpoints.OBTENER_POR_ID, async (int id, IFuncionarioService service) =>
        {
            try
            {
                var funcionario = await service.ObtenerFuncionarioPorIdAsync(id);
                if (funcionario == null)
                    return Results.NotFound();

                return Results.Ok(funcionario);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        }).Produces<FuncionarioEditDTO>(StatusCodes.Status200OK);

        funcionarios.MapGet(FuncionarioEndpoints.OBTENER_TODOS, async (IFuncionarioService service) =>
        {
            try
            {
                var listFuncionarios = await service.ObtenerTodosLosFuncionariosAsync();
                return Results.Ok(listFuncionarios);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        }).Produces<List<FuncionarioEditDTO>>(StatusCodes.Status200OK);

        funcionarios.MapPut(FuncionarioEndpoints.ACTUALIZAR_DATOS, async (FuncionarioEditDTO funcionarioRegister, IFuncionarioService service) =>
        {
            try
            {
                var funcionarioActualizado = await service.ActualizarFuncionarioAsync(funcionarioRegister);
                return funcionarioActualizado ? Results.Ok() : Results.BadRequest(new { error = "No se pudo actualizar el funcionario." });
            }
            catch (ArgumentException argEx)
            {
                return Results.BadRequest(new { error = argEx.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        }).Produces<bool>(StatusCodes.Status200OK);
    }
}
