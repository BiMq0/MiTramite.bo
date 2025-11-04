using MiTramite_Back.Logica_De_Negocio.Services.RentistaSvc;
using MiTramite_Shared.DTOs.RentistaDTOs;
using MiTramite_Shared.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace MiTramite_Back;

public static class RentistaMapper
{
    public static void Map(this WebApplication app)
    {
        var rentistas = app.MapGroup(RentistaEndpoints.BASE);

        rentistas.MapPost(RentistaEndpoints.SIGNUP, async ([FromBody] RentistaSignupDTO rentistaNuevo, [FromServices] IRentistaService service) =>
        {
            return await service.RegistrarNuevoRentista(rentistaNuevo);
        });

        rentistas.MapPost(RentistaEndpoints.LOGIN, async ([FromBody] RentistaLoginDTO rentistaLogin, [FromServices] IRentistaService service) =>
        {
            return await service.IniciarSesionRentista(rentistaLogin);
        });
    }
}
