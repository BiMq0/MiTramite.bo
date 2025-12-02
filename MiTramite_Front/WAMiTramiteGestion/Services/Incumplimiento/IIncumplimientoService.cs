using System.Collections.Generic;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.Incumplimiento;

namespace WAMiTramiteGestion.Services.Incumplimiento
{
    public interface IIncumplimientoService
    {
        Task<List<IncumplimientoRegistroDTO>> ObtenerTodosAsync();
        Task<IncumplimientoDetalleDTO?> ObtenerPorIdTramiteAsync(long idSolicitudTramite);
    }
}
