using System;
using System.Threading.Tasks;

namespace WAMiTramiteGestion.Services.Notificaciones
{
    public interface INotificacionService
    {
        event Action<object>? OnIncumplimiento;
        event Action<object>? OnReasignacion;
        Task IniciarConexionAsync();
    }
}