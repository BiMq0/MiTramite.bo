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
            var dto = await service.IniciarSesionFuncionario(funcionarioLogin);
            if (dto == null) return Results.Unauthorized();

            return Results.Ok(dto);
        });
    }
}
