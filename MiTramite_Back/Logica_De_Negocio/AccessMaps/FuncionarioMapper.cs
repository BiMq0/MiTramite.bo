using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc;

namespace MiTramite_Back.AccessMaps;

public static class FuncionarioMapper
{
    public static void Map(this WebApplication app)
    {
        var funcionarios = app.MapGroup(FuncionarioEndpoints.BASE);

        funcionarios.MapPost(FuncionarioEndpoints.LOGIN, async (FuncionarioLoginDTO funcionarioLogin, IFuncionarioService service) =>
        {
            try
            {
                var dto = await service.IniciarSesionFuncionario(funcionarioLogin);
                if (dto == null) return Results.Unauthorized();

                return Results.Ok(dto);
            }
            catch (KeyNotFoundException knfEx)
            {
                return Results.NotFound(new { Message = knfEx.Message });
            }
            catch (UnauthorizedAccessException uaEx)
            {
                return Results.Unauthorized();
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        });

        funcionarios.MapPost(FuncionarioEndpoints.REGISTER, async (FuncionarioNuevoDTO funcionarioRegister, IFuncionarioService service) =>
        {
            try
            {
                var dto = await service.CrearFuncionarioAsync(funcionarioRegister);
                return Results.Ok(dto);
            }
            catch (ArgumentException argEx)
            {
                return Results.BadRequest(new { Message = argEx.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        });

        funcionarios.MapGet(FuncionarioEndpoints.OBTENER_POR_ID, async (int id, IFuncionarioService service) =>
        {
            try
            {
                var dto = await service.ObtenerFuncionarioPorIdAsync(id);
                if (dto == null) return Results.NotFound();

                return Results.Ok(dto);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        });

        funcionarios.MapGet(FuncionarioEndpoints.OBTENER_TODOS, async (IFuncionarioService service) =>
        {
            try
            {
                var dtos = await service.ObtenerTodosLosFuncionariosAsync();
                return Results.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        });
    }
}
