using System.Collections.Generic;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.Incumplimiento;

namespace MiTramite_Back.Logica_De_Negocio.Services.Incumplimiento
{
    public interface IIncumplimientoService
    {
        Task<List<IncumplimientoRegistroDTO>> ObtenerTodosAsync();
        Task<IncumplimientoDetalleDTO?> ObtenerPorIdTramiteAsync(long idSolicitudTramite);
    }
}
