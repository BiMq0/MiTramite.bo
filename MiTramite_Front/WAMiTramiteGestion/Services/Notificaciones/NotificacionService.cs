using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using WAMiTramiteGestion.Handlers;

namespace WAMiTramiteGestion.Services.Notificaciones
{
    public class NotificacionService : INotificacionService
    {
        private HubConnection? _connection;
        public event Action<object>? OnIncumplimiento;
        public event Action<object>? OnReasignacion;
        public async Task IniciarConexionAsync()
        {
            if (_connection is not null && _connection.State == HubConnectionState.Connected)
            {
                Console.WriteLine("[SignalR] Ya existe una conexión activa.");
                return;
            }

            var url = Config.ApiUrl + "notificacionesHub";
            Console.WriteLine($"[SignalR] Conectando a: {url}");

            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            _connection.On<object>("NotificacionIncumplimiento", mensaje =>
            {
                Console.WriteLine($"[SignalR] Mensaje Incumplimiento: {mensaje}");
                OnIncumplimiento?.Invoke(mensaje);
            });

            _connection.On<object>("NotificacionReasignacion", mensaje =>
            {
                Console.WriteLine($"[SignalR] Mensaje Reasignacion: {mensaje}");
                OnReasignacion?.Invoke(mensaje);
            });

            try
            {
                await _connection.StartAsync();
                Console.WriteLine($"[SignalR] Conectado. ID: {_connection.ConnectionId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar SignalR: {ex.Message}");
                throw;
            }
        }
    }
}