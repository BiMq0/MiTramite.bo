using MiTramite_Shared.Endpoints;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MiTramite_Back.AccessMaps;

public static class FuncionarioMapper
{
    public static void Map(this WebApplication app)
    {
        var funcionarios = app.MapGroup(FuncionarioEndpoints.BASE);

        funcionarios.MapPost(FuncionarioEndpoints.LOGIN, async ([FromBody] FuncionarioLoginDTO funcionarioLogin, [FromServices] IFuncionarioService service, HttpContext http) =>
        {
            var dto = await service.IniciarSesionFuncionario(funcionarioLogin);
            if (dto == null) return Results.Unauthorized();

            var json = JsonSerializer.Serialize(dto);
            http.Session.SetString("FuncionarioAccesos", json);

            return Results.Ok(dto);
        });
    }
}
