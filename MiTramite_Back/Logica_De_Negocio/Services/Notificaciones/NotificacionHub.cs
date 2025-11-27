using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MiTramite_Back.Logica_De_Negocio.Services.Notificaciones
{
    public class NotificacionesHub : Hub
    {
        public async Task EnviarNotificacionIncumplimiento(string mensaje)
        {
            await Clients.All.SendAsync("NotificacionIncumplimiento", mensaje);
        }

        public async Task EnviarNotificacionReasignacion(string mensaje)
        {
            await Clients.All.SendAsync("NotificacionReasignacion", mensaje);
        }
    }

}