using System;
using System.Collections.Generic;

namespace MiTramite_Shared.DTOs.Reportes
{
    public class ReporteTramitesPorMesDTO
    {
        public string Mes { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class ReporteRentistasPorEdadDTO
    {
        public string RangoEdad { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class ReporteEstadoTramitesDTO
    {
        public string Estado { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class ReporteIncumplimientosFuncionarioDTO
    {
        public string NombreFuncionario { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class ReporteDashboardDTO
    {
        public List<ReporteTramitesPorMesDTO> TramitesPorMes { get; set; } = new();
        public List<ReporteRentistasPorEdadDTO> RentistasPorEdad { get; set; } = new();
        public List<ReporteEstadoTramitesDTO> EstadosTramites { get; set; } = new();
        public List<ReporteIncumplimientosFuncionarioDTO> IncumplimientosTop { get; set; } = new();
    }
}
