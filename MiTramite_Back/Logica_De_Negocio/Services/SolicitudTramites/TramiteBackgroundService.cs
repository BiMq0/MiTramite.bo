using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MiTramite_Back.Acceso_A_Datos.Repositories.IncumplimientoRep;
using MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramitesRep;
using MiTramite_Back.Logica_De_Negocio.Services.EmailSvc;
using MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc;
using MiTramite_Back.Logica_De_Negocio.Services.Notificaciones;
using MiTramite_Back.Logica_De_Negocio.Services.SolicitudTramitesSvc;
using MiTramite_Domain.Constants;

namespace MiTramite_Back.Logica_De_Negocio.Services.SolicitudTramites
{
    public class TramiteBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TramiteBackgroundService> _logger;

        public TramiteBackgroundService(IServiceScopeFactory scopeFactory, ILogger<TramiteBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var tramiteRepository = scope.ServiceProvider.GetRequiredService<ISolicitudTramiteRepository>();
                var incumplimientoRepository = scope.ServiceProvider.GetRequiredService<IIncumplimientoRepository>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<NotificacionesHub>>();

                var tramites = await tramiteRepository.ObtenerTramitesParaBackgroundServiceAsync();

                foreach (var tramite in tramites)
                {
                    if (tramite.FechaEstimadaEntrega <= DateTime.Now)
                    {
                        // Registrar incumplimiento
                        var incumplimiento = await incumplimientoRepository.RegistrarIncumplimiento(tramite);

                        // Reasignar funcionario
                        var funcionarioNuevo = await tramiteRepository.ObtenerFuncionarioConMayorDisponibilidadAsync(stoppingToken);
                        if (funcionarioNuevo == null)
                        {
                            _logger.LogWarning("No hay funcionarios disponibles para reasignar el trámite {TramiteId}", tramite.IdSolicitudTramite);
                            return;
                        }


                        await emailService.EnviarCorreoNotificacionFuncionarioInfractor(tramite.Funcionario.Correo, tramite);

                        // Notificaciones en tiempo real (SignalR)
                        await hubContext.Clients.All.SendAsync("NotificacionIncumplimiento", new
                        {
                            Mensaje = $"El funcionario {tramite.Funcionario!.Nombres} {tramite.Funcionario.ApellidoPaterno} {tramite.Funcionario.ApellidoMaterno ?? ""} incumplió el trámite {tramite.IdSolicitudTramite}"
                        });

                        tramite.IdFuncionario = funcionarioNuevo.IdFuncionario;
                        tramite.FechaSolicitud = DateTime.Now;
                        tramite.IdEstadoTramite = (int)TramiteEstados.Urgente;

                        await tramiteRepository.ActualizarTramitePorIncumplimiento(tramite);

                        await emailService.EnviarCorreoReasignacionFuncionarioNuevo(funcionarioNuevo.Correo, tramite);


                        await hubContext.Clients.All.SendAsync("NotificacionReasignacion", new
                        {
                            Mensaje = $"El trámite {tramite.IdSolicitudTramite} fue reasignado al funcionario {funcionarioNuevo.Nombres} {funcionarioNuevo.ApellidoPaterno} {funcionarioNuevo.ApellidoMaterno ?? ""}."
                        });
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}