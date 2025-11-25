using MiTramite_Shared.DTOs.RentistaDTOs;
namespace WAMiTramite.Services;

public interface IRentistaService
{
    RentistaCurrentDataDTO? rentistaCurrentData { get; set; }
    Task<RentistaCurrentDataDTO> IniciarSesionRentista(RentistaLoginDTO rentistaLoginDTO);
    Task<bool> RegistrarRentista(RentistaSignupDTO rentistaSignupDTO);
}
