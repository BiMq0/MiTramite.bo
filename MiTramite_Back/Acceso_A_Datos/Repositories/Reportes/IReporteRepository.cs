using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.Reportes;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.Reportes
{
    public interface IReporteRepository
    {
        Task<List<ReporteTramitesPorMesDTO>> GetTramitesPorMesAsync(int year);
        Task<List<ReporteRentistasPorEdadDTO>> GetRentistasPorEdadAsync();
        Task<List<ReporteEstadoTramitesDTO>> GetEstadoTramitesAsync();
        Task<List<ReporteIncumplimientosFuncionarioDTO>> GetIncumplimientosTopAsync();
    }
}
