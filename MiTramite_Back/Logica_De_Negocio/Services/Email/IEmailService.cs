using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.EmailSvc
{
    public interface IEmailService
    {
        Task<bool> EnviarCorreoAsync(string destinatario, string asunto, string cuerpo, string? correoEmisor = null, CancellationToken cancellationToken = default);
        Task<bool> NotificarCompletacionTramiteAsync(string correoRentista, string nombreRentista, string nombreTramite, string correoFuncionario, CancellationToken cancellationToken = default);
        Task<bool> NotificarRechazoTramiteAsync(string correoRentista, string nombreRentista, string nombreTramite, string motivo, string correoFuncionario, CancellationToken cancellationToken = default);
        Task<bool> EnviarBienvenidaRentistaAsync(string correoRentista, string nombreRentista, CancellationToken cancellationToken = default);
        Task EnviarCorreoNotificacionFuncionarioInfractor(string correoFuncionarioInfractor, SolicitudTramite incumplimiento);
        Task EnviarCorreoReasignacionFuncionarioNuevo(string correoFuncionarioNuevo, SolicitudTramite tramite);
    }
}
