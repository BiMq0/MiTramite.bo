using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiTramite_Shared.DTOs.Incumplimiento
{
    public class IncumplimientoRegistroDTO
    {
        public long IdSolicitudTramite { get; set; }
        public string NombreRentista { get; set; } = string.Empty;
        public string NombreFuncionarioOriginal { get; set; } = string.Empty;
        public string NombreFuncionarioReasignado { get; set; } = string.Empty;
        public DateTime FechaIncumplimiento { get; set; }
    }

    public class IncumplimientoDetalleDTO : IncumplimientoRegistroDTO
    {
        public string NombreTipoTramite { get; set; } = string.Empty;
        public string CorreoRentista { get; set; } = string.Empty;
        public string CorreoFuncionarioOriginal { get; set; } = string.Empty;
        public string CorreoFuncionarioReasignado { get; set; } = string.Empty;
        public DateTime FechaSolicitudTramite { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
    }
}
